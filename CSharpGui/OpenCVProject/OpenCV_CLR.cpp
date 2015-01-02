#include "openCV.h"
#include <string>

using namespace System::Runtime::InteropServices;

#pragma managed

namespace NS_OpenCV
{

	public ref class OpenCvCLR
	{
	public:
		OpenCvCLR() {}
		~OpenCvCLR() {}


		System::String^ runMSER_CLR(System::String^ sFilePath, int delta, int max_variation, int min_diversity)
		{
			char * szUnManagedFilePath = (char*)(void*)Marshal::StringToHGlobalAnsi(sFilePath);
			std::string sUnamangedFilePath(szUnManagedFilePath);
			std::string cropsPath = runMSER(delta,max_variation,min_diversity,sUnamangedFilePath);
			Marshal::FreeHGlobal( System::IntPtr(szUnManagedFilePath));
			return gcnew System::String(cropsPath.c_str());

		}

		array<System::String^>^ runGPSCoords_CLR(float yaw, float pitch, float roll,float LatO, float LongO, float AltO, float LatC, float LongC, float AltC, int xPixel, int yPixel)
		 {
			 array<System::String^>^ finalResult = gcnew array<System::String^>(4);
			 std::string* result = new std::string[4];
			 FindRealWorldVectorFinal( yaw,  pitch,  roll, LatO,  LongO,  AltO,  LatC,  LongC,  AltC,  xPixel,  yPixel, result);
			 for (int i = 0 ; i < 4 ; i++)
			 {
				 finalResult[i] = gcnew System::String(result[i].c_str());
			 }
			 return finalResult;
		 }

	}; 

};


