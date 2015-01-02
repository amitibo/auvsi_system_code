#include "openCV.h"
#include <stdio.h>
#include "opencv2/core/core.hpp"
#include "opencv2/imgproc/imgproc.hpp"
#include "opencv2/features2d/features2d.hpp"
#include "opencv2/objdetect/objdetect.hpp"
#include "opencv2/calib3d/calib3d.hpp"
#include "opencv2/ml/ml.hpp"
#include "opencv2/highgui/highgui.hpp"
#include <math.h>
#include <iostream>
#include <iomanip>

using namespace cv;
using namespace std;

#define _USE_MATH_DEFINES

#define d2r ( M_PI / 180.0 )

const float PI = 3.14159265358979323846;
const float fd2r = ( PI / 180.0 );
const float EARTH_RADIUS_KM = 6371.0; //or 6367??
const float EARTH_RADIUS_M = 6371000.0; //or 6367??
const float POLAR_RADIUS = 6356750.0;
const float EQUATORIAL_RADIUS=6378200.0;


extern "C"
{
	__declspec(dllexport) std::string __cdecl runMSER(int _delta, int _max_variation, int _min_diversity, std::string filename);

	//GPS
	__declspec(dllexport) void __cdecl   FindRealWorldVectorFinal(float yaw, float pitch, float roll,float LatO, float LongO, float AltO, float LatC, float LongC, float AltC, int xPixel, int yPixel, string* result);
}


__declspec(dllexport) std::string __cdecl runMSER(int _delta, int _max_variation, int _min_diversity, std::string filename)
{
	/*cv::Mat img_hsv,img_rgb;
	img_rgb = cv::imread(filePath,1); 
	cvtColor(img_rgb, img_hsv, CV_RGB2HSV);
	cv::imwrite( filePath, img_hsv );*/

		//ostringstream stringStream;
	//stringStream << "C:/pics/" << imgName[imgNumber] << ".jpg"; 
	//string filename = stringStream.str();

	
	//compression parameters
	std::vector<int> compression_params; //vector that stores the compression parameters of the image


	/// Load source image and convert it to gray
	cv::Mat src = cv::imread(filename);
	cv::Mat img_hsv;
	cv::cvtColor(src, img_hsv, CV_BGR2HSV);
	cv::vector<cv::Mat> hsv_channels; 
	split(src, hsv_channels);
	cv::Mat h = hsv_channels[0];
	cv::Mat s = hsv_channels[1];
	cv::Mat v2 = hsv_channels[2].clone();
	cv::Vec3b hsvPixel = img_hsv.at<cv::Vec3b>(0, 0);
	cv::Mat v = cv::Mat::ones(cv::Size(src.cols, src.rows), CV_8UC1) * 255;
	std::vector<cv::Mat> channels;

	cv::Mat fin_img;

	channels.push_back(s);
	channels.push_back(h);
	channels.push_back(v);

	hsv_channels.pop_back();
	hsv_channels.push_back(v);

	merge(channels, fin_img);
	//Mat img_rgb, img_gray;

	//cvtColor(fin_img, img_rgb, CV_HSV2RGB);
	//cvtColor(img_rgb, img_gray, CV_RGB2GRAY);

	std::vector<std::vector<cv::Point>> contours;

	/* int _delta=5, 
	int _min_area=60, 
	int _max_area=14400,
	double _max_variation=0.25, 
	double _min_diversity=0.2,
	int _max_evolution=200, 
	double _area_threshold=1.01,
	double _min_margin=0.003, 
	int _edge_blur_size=5*/
	cv::MSER(_delta, 60, 14400, (double)(_max_variation)/100.0, (double)(_min_diversity)/10.0, 200, 1.01, 0.003, 5)(s, contours);

	std::vector<cv::Rect> minRect(contours.size());
	for (unsigned int i = 0; i < contours.size(); i++)
	{
		minRect[i] = cv::boundingRect(cv::Mat(contours[i]));
	}
	
	groupRectangles(minRect,1,1.5);
	std::vector<cv::Rect> squares(minRect);
	for (unsigned int i = 0 ; i<minRect.size() ; i++)
	{
			/*rectToSquare(squares[i], 3);*/
			cv::Rect rect = squares[i];
			double alpha = 3;
			int x = rect.x;
		int y = rect.y;
		int width = rect.width;
		int height = rect.height;
		int x_center = x+(int)width/2;
		int y_center = y+(int)height/2;
		int max = width;
		if (height<width)
			max = height;
		int x_new = 0;
		int y_new = 0;
		if (x_center-alpha*max/2 > 0)
			x_new = (int)(x_center-alpha*max/2);
		if (x_new+(int)max*alpha > src.size().width)
			max = (int)(src.size().width - x_new)/alpha;
		if (y_center-alpha*max/2 > 0)
			y_new = (int)(y_center-alpha*max/2);
		if (y_new+(int)max*alpha > src.size().height)
			max = (int)(src.size().height - y_new)/alpha;
		rect = cv::Rect(x_new,y_new,(int)(max*alpha),(int)(max*alpha));
		//end of rect to square

		std::string::iterator it;
		std::string::iterator last;
		unsigned int index=0, last_index=0, dot_index=0;
		for (it = filename.begin(); it < filename.end(); it++) 
		{
			index++;
			if (*it == '\\')
			{
				last = it;
				last_index=index;
			}
			if (*it == '.')
			{
				dot_index=index;
			}
		}
		std::string imagename = filename.substr(last_index,dot_index-last_index-1);


		//saveSquare(squares,i,"C:/output/");
		cv::Point2i center = cv::Point2i(squares[i].x+(int)squares[i].width/2, squares[i].y+(int)squares[i].height/2);
		std::ostringstream stringStream;
		/*stringStream << "C:/output/img" << "_" << i+1 << "_(" << center.x << "," << center.y << ").jpg";*/ 
		stringStream << "C:\\temp\\output\\" << imagename << "_cr" << i << "_t(" << center.x << "," << center.y << ").jpg"; 
		std::string path = stringStream.str();

		cv::Mat croppedImage = src(squares[i]);
		bool success = imwrite(path, croppedImage, compression_params); //write the image to file
		if (!success)
		{ 
			return "";
		}
		//end of save square


	}
	/*draw_contours(contours, squares);*/

	//Draw contours + rotated rects
	cv::Mat drawing = cv::Mat::zeros(src.size(), CV_8UC3);
	cv::RNG rng(12345);
	for (unsigned int i = 0; i<squares.size(); i++)
	{

		cv::Scalar color = cv::Scalar(rng.uniform(0, 255), rng.uniform(0, 255), rng.uniform(0, 255));
		// contour
		//drawContours(drawing, contours, i, color, 1, 8, vector<Vec4i>(), 0, Point());

		int x = squares[i].x;
		int y = squares[i].y;
		int width = squares[i].width;
		int height = squares[i].height;
		cv::Point2f rect_points[4];
		rect_points[0] = cv::Point(x, y);
		rect_points[1] = cv::Point(x + width, y);
		rect_points[2] = cv::Point(x + width, y + height);
		rect_points[3] = cv::Point(x, y + height);

		for (int j = 0; j < 4; j++)
			line(src, rect_points[j], rect_points[(j + 1) % 4], color, 2, 8);
	}

	/// Show in a window
	//cv::namedWindow("window", CV_WINDOW_NORMAL);
	//imshow("window", src);

	//end of draw contours

	return "";
}




//GPS


cv::Mat* CreateCalibrationMatrix( float f, float cx, float cy)
{
	//cv::Mat CalibrationMat = new cv::Mat();


	float calibMatData[] = {	-f,	0,	cx,
								0,	f,	cy,
								0,	0,	1		};
	cv::Mat* CalibrationMat = new Mat(3,3,CV_32FC1,&calibMatData);

	return CalibrationMat;
}

cv::Mat CreateRotationMatrix( float yaw, float pitch, float roll)
{
	cv::Mat yawMatrix;
	cv::Mat pitchMatrix;
	cv::Mat rollMatrix;
	
	cv::Mat RotationMat;

	float yawMatData[] =  {	cos(yaw*fd2r),	sin(yaw*fd2r),	0, 
							-sin(yaw*fd2r),	cos(yaw*fd2r),	0, 
								0,			0,		1		};
	yawMatrix = Mat(3,3,CV_32FC1,&yawMatData);
	//cout<<yawMatrix<<endl;
	
	float rollMatData[] = {	cos(roll*fd2r) ,	0	,	sin(roll*fd2r), 
									0	     ,	1	,		0, 
							-sin(roll*fd2r),	0	,	cos(roll*fd2r)	};
	rollMatrix = Mat(3,3,CV_32FC1,&rollMatData);
	//cout<<rollMatrix<<endl;

	float pitchMatData[] ={		1,			0		,		0,
								0,	cos(pitch*fd2r),	-sin(pitch*fd2r), 
								0,	sin(pitch*fd2r),	cos(pitch*fd2r)		};
	pitchMatrix = Mat(3,3,CV_32FC1,&pitchMatData);
	//cout<<pitchMatrix<<endl;

//	RotationMat = rollMatrix * pitchMatrix * yawMatrix;
//	RotationMat = yawMatrix * pitchMatrix * rollMatrix;
//	RotationMat = pitchMatrix * rollMatrix * yawMatrix;
//	RotationMat =  pitchMatrix * yawMatrix * rollMatrix;
	RotationMat =  yawMatrix * rollMatrix * pitchMatrix;

	return RotationMat;
}

//the point components are: x - x-axis distance
//							y -	y-axis distance
//							z - altitude
cv::Point3f CalculateMetricCoords ( cv::Point3f gpsStartCoords , cv::Point3f gpsCurrentCoords )
{
	cv::Point3f distanceVec;
	double a, c, d;

	double latDist = (gpsCurrentCoords.x - gpsStartCoords.x) * fd2r;
    double longDist = (gpsCurrentCoords.y - gpsStartCoords.y) * fd2r;
	
	//calculate the x-axis distance. canceling the long distance
	double zeroLong = 0.0;
	a = pow(sin(latDist/2.0), 2) + cos( gpsStartCoords.x * fd2r) * cos(gpsCurrentCoords.x * fd2r) * pow(sin(zeroLong/2.0), 2);
    c = 2 * atan2(sqrt(a), sqrt(1-a));
    d = EARTH_RADIUS_KM * c;
	distanceVec.x = (float)d * 1000;
	if (latDist < 0)
	{
		distanceVec.x *= -1;
	}
	

	//calculate the y-axis distance. canceling the lat distance
	double zeroLat = 0.0;
	a = pow(sin(zeroLat/2.0), 2) + cos( gpsStartCoords.x * fd2r) * cos(gpsCurrentCoords.x * fd2r) * pow(sin(longDist/2.0), 2);
    c = 2 * atan2(sqrt(a), sqrt(1-a));
    d = EARTH_RADIUS_KM * c;
	distanceVec.y = (float)d * 1000;
	if (longDist < 0)
	{
		distanceVec.y *= -1;
	}

	distanceVec.z = abs(gpsStartCoords.z - gpsCurrentCoords.z);

	//calculate the general distance - remains for further use if needed.
	/*
	a = pow(sin(latDist/2.0), 2) + cos( gpsStartCoords.x * fd2r) * cos(gpsCurrentCoords.x * fd2r) * pow(sin(longDist/2.0), 2);
    c = 2 * atan2(sqrt(a), sqrt(1-a));
    d = EARTH_RADIUS_KM * c;
	distanceVec.z = (float)d * 1000;
	*/

	return distanceVec;
	
}


cv::Point2i CenterImage (cv::Point2i pixelCoords, int imageWidth, int imageHeight)
{
	cv::Point2i newCoor;
	
	newCoor.x = pixelCoords.x - (imageWidth / 2);
	newCoor.y = pixelCoords.y - (imageHeight / 2);

	return newCoor;
}//cancel!!   


//same as kinv * fx+cx vec... => calculated in the FindImageVector function
/*
cv::Point3f GPS2Pixel::FindImageVector (cv::Point2i centeredPixelCoords, float f, float cx, float cy)
{
	cv::Point3f imageVector;


	imageVector.x = ((float)centeredPixelCoords.x - cx) / f;
	imageVector.y = ((float)centeredPixelCoords.y - cy) / f;
	imageVector.z = 1;

	return imageVector;
}*/

cv::Point3f FindImageVector (cv::Point2i pixelCoords  /*, cv::Mat calib*/)
{
	float calibMatData[] = {	6989,	0,	2000,
								0,	-6989,	1500,
								0,	0,	1		};
	cv::Mat CalibrationMat = Mat(3,3,CV_32FC1,&calibMatData);

	cv::Point3f pixelCoordsWith1;
	pixelCoordsWith1.x = pixelCoords.x;
	pixelCoordsWith1.y = pixelCoords.y;
	pixelCoordsWith1.z = 1;
	
	cv::Mat pixelCoordMat(pixelCoordsWith1 , true);

	//cout<<pixelCoordMat<<endl;
	//cout<<CalibrationMat<<endl;

	//cout<<CalibrationMat.inv()<<endl;

	cv::Mat imageVectorMat = CalibrationMat.inv() * pixelCoordMat;
	//cout << imageVectorMat << endl;

	cv::Point3f imageVector;
	imageVector.x = imageVectorMat.at<float>(0);
	imageVector.y = imageVectorMat.at<float>(1);
	imageVector.z = imageVectorMat.at<float>(2) * (-1);

	return imageVector;
} 


//G + n^ * t
//Metric Coords : G = (x = x-axis, y = y-axis, z = altitude)
cv::Point3f FindRealWorldVector (cv::Point3f metricCoords, cv::Point3f TargetRayVec, cv::Mat rotationMatrix)
{
	cv::Mat tempVec;
	cv::Mat metricMat(metricCoords , true); //used to be matric vector
	cv::Mat TargetRayMat(TargetRayVec , true);  //used to be image vector
	cv::Mat rotatedVec;
	cv::Point3f realWorldVector;

	//cout << metricMat << endl;
	//cout << TargetRayMat << endl;
	//cout << rotationMatrix << endl;
	 
	rotatedVec = rotationMatrix * TargetRayMat;
	//cout << rotatedVec << endl;
	float TFactor = ( - metricCoords.z / rotatedVec.at<float>(2) );
	tempVec = metricMat + ( rotatedVec * TFactor);
	
	//cout << rotatedVec * TFactor << endl;
	//cout << tempVec << endl;

	//build the answer vactor
	realWorldVector.x = tempVec.at<float>(0);
	realWorldVector.y = tempVec.at<float>(1);
	realWorldVector.z = tempVec.at<float>(2);

	return realWorldVector;

}


cv::Point2f TargetGPSCoordsCalc (cv::Point3f gpsCurrentCoords, cv::Point3f metricRealWorldVector)
{
	cv::Point2f resTargetGPSCoords;

//	resTargetGPSCoords.x = gpsCurrentCoords.x + ((180/PI)*(metricRealWorldVector.y / EARTH_RADIUS_M));
//	resTargetGPSCoords.y = gpsCurrentCoords.y + ((180/PI)*(metricRealWorldVector.x / (EARTH_RADIUS_M * cos(gpsCurrentCoords.x*fd2r)))); 
	resTargetGPSCoords.x = gpsCurrentCoords.x + ((180/PI)*(metricRealWorldVector.y / POLAR_RADIUS));
	resTargetGPSCoords.y = gpsCurrentCoords.y + ((180/PI)*(metricRealWorldVector.x / (EQUATORIAL_RADIUS * cos(gpsCurrentCoords.x*fd2r)))); 
	return resTargetGPSCoords;

}

string ConvertToMinuteString(float TargetCoordDeg)
{
	float absTargetCoord = abs(TargetCoordDeg);
	double deg;
	double minutes;
	double secs;
	double tenths;
	std::ostringstream sDeg;
	std::ostringstream sMinutes;
	std::ostringstream sSecs;
//	std::ostringstream sTenths;
	string resString;
	

	deg = (int)(absTargetCoord);
	minutes = (absTargetCoord - floor(absTargetCoord)) * 60.0;
	secs = (minutes - floor(minutes)) * 60.0;
	tenths = (secs - floor(secs)) * 10.0;
	
	sDeg << deg; 
	sMinutes << floor(minutes);
	sSecs << std::fixed << std::setprecision(3) << secs;

	resString = sDeg.str() + " " + sMinutes.str() + " " + sSecs.str();// + " " + sTenths.str();
	return resString;


}

void FinalResAsMinutes(float* targetCoords, string* TargetLatMinute, string* TargetLonMinute)
{
	string tempStr;
	//create Lat string
	tempStr = ConvertToMinuteString(targetCoords[0]);
	if ( targetCoords[0] < 0 )
	{
		*TargetLatMinute = "S" + tempStr;
	}
	else
	{
		*TargetLatMinute = "N" + tempStr;
	}

	//create Lon string
	tempStr = ConvertToMinuteString(targetCoords[1]);
	if ( targetCoords[1] < 0 )
	{
		*TargetLonMinute = "W" + tempStr;
	}
	else
	{
		*TargetLonMinute = "E" + tempStr;
	}

}

//float* GPS2Pixel::FindRealWorldVector(float yaw, float pitch, float roll,float LatO, float LongO, float AltO, float LatC, float LongC, float AltC, int xPixel, int yPixel)
__declspec(dllexport) void __cdecl  FindRealWorldVectorFinal(float yaw, float pitch, float roll,float LatO, float LongO, float AltO, float LatC, float LongC, float AltC, int xPixel, int yPixel, string* result)
{


	cv::Point3f startGPS;
	cv::Point3f currentGPS;
	cv::Point2i pixel;
	std::ostringstream LatDeg;
	std::ostringstream LonDeg;
	string TargetLatMinute;
	string TargetLonMinute;
	string resAsMinutes[2]; 

	startGPS.x = LatC;
	startGPS.y = LongC;
	startGPS.z = AltO;
	currentGPS.x = LatC;
	currentGPS.y = LongC;
	currentGPS.z = AltC;
	pixel.x = xPixel;
	pixel.y = yPixel;

	cv::Mat RotMat = CreateRotationMatrix(yaw, pitch, roll);
	cv::Point3f distance = CalculateMetricCoords ( startGPS , currentGPS );
	cv::Point3f imageVector = FindImageVector(pixel);
	cv::Point3f realWorldVector = FindRealWorldVector(distance , imageVector , RotMat);
	cv::Point2f resTargetGPSCoords = TargetGPSCoordsCalc (currentGPS, realWorldVector); //maybe currentGPS

	float GPSres[2];
	GPSres[0] = resTargetGPSCoords.x; //LAT
	GPSres[1] = resTargetGPSCoords.y; //LONG

	FinalResAsMinutes(GPSres, &TargetLatMinute, &TargetLonMinute);
//	resAsMinutes[0] = TargetLatMinute;
//	resAsMinutes[1] = TargetLonMinute;
	
	result[2] = TargetLatMinute; //latitude minute
	result[3] = TargetLonMinute; //longitude minute

	LatDeg << std::fixed << std::setprecision(6) << GPSres[0];
	LonDeg << std::fixed << std::setprecision(6) << GPSres[1];

	result[0] = LatDeg.str();//latitude deg
	result[1] = LonDeg.str();//longitude deg

//	return resAsMinutes;



}



