
#ifndef _TESTDLL_H_
#define _TESTDLL_H_



//#include "stdafx.h"
#include <stdio.h>
#include <iostream>


#ifdef OPEN_CV_EXPORTS
#define OPENCV_API __declspec(dllexport)
#else
#define OPENCV_API __declspec(dllimport)
#endif


extern "C"
{
	OPENCV_API std::string __cdecl runMSER(int _delta, int _max_variation, int _min_diversity, std::string filename);
	//GPS
	/*
	OPENCV_API cv::Mat*	 __cdecl CreateCalibrationMatrix( float f, float cx, float cy );
	OPENCV_API cv::Mat	 __cdecl 	CreateRotationMatrix ( float yaw, float pitch, float roll );

	OPENCV_API cv::Point3f __cdecl  CalculateMetricCoords ( cv::Point3f gpsStartCoords , cv::Point3f gpsCurrentCoords );
	
	OPENCV_API cv::Point2i __cdecl  CenterImage ( cv::Point2i pixelCoor, int imageWidth, int imageHeight );
	
	OPENCV_API cv::Point3f __cdecl  FindImageVector ( cv::Point2i centeredPixelCoor);
	
	OPENCV_API cv::Point3f __cdecl  FindRealWorldVector (cv::Point3f metricCoords, cv::Point3f ImageCoords, cv::Mat rotationMatrix);


	OPENCV_API cv::Point2f __cdecl  TargetGPSCoordsCalc (cv::Point3f gpsCurrentCoords, cv::Point3f metricRealWorldVector);
	OPENCV_API cv::Point2f __cdecl  TargetGPSCoords (cv::Point3f gpsStartCoords, cv::Point3f metricRealWorldVector);*/

	OPENCV_API void __cdecl    FindRealWorldVectorFinal(float yaw, float pitch, float roll,float LatO, float LongO, float AltO, float LatC, float LongC, float AltC, int xPixel, int yPixel, std::string* result);

}

#endif