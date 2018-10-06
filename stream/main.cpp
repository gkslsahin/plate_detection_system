#include <opencv2/core/core.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/imgproc/imgproc.hpp>
#include <opencv2/photo/photo.hpp>
#include <iostream>
#include "alpr.h"

//#include "tick_meter.hpp"

using namespace cv;
using namespace std;
using namespace alpr;

//  ( "eu", "au", or "kr")
alpr::Alpr openalpr("eu", "/etc/openalpr/openalpr.conf");

std::vector<AlprRegionOfInterest> regionsOfInterest;

std::string filename1 = "v4l2src device=/dev/video0 ! video/x-raw,width=1280,height=720,format=BGR,framerate=30/1 ! appsink";

Mat image,image2;

void unsharpMask(cv::Mat& im)
{
    cv::Mat tmp;
    //cv::GaussianBlur(im, tmp, cv::Size(5,5), 5,5);
    //cv::addWeighted(im, 1.5, tmp, -0.5, 0, im);

    cv::GaussianBlur(im, tmp, cv::Size(1,1), 1,1);
    //bilateralFilter(im, tmp, 10, 20, 5,BORDER_DEFAULT );
    cv::addWeighted(im, 1.5, tmp, -0.4, 0, im);


}

Mat laplacian_sharp (Mat img)
{
   Mat imgLaplacian,imgResult;
   Mat kernel = (Mat_<float>(3,3) <<
        0,  1, 0,
        1, -4, 1,
        0,  1, 0);

    /*Mat kernel = (Mat_<float>(3,3) <<
        1,  1, 1,
        1, -8, 1,
        1,  1, 1); // another approximation of second derivate, more stronger
    */

    filter2D(img, imgLaplacian, CV_32F, kernel);
    img.convertTo(img, CV_32F);
    imgResult = img - imgLaplacian;
    imgResult.convertTo(imgResult, CV_8U);
    return imgResult;
}

Mat adjust_HSV (Mat img,int hsv_value)
{
    vector<Mat> channels;
    cvtColor(img, img, CV_BGR2HSV);
    split(img,channels);
    //channels[0]=channels[0]+ Scalar(hsv_value, hsv_value, hsv_value);
	channels[1]=channels[1]+ Scalar(hsv_value, hsv_value, hsv_value);
	//channels[2]=channels[2]+ Scalar(hsv_value, hsv_value, hsv_value);
	merge(channels,img);
    cvtColor(img, img, CV_HSV2BGR);
    return img;
}


int main( int argc, char** argv )
{
    //TickMeter tm;

    VideoCapture cap(0); // open the default camera
    if(!cap.isOpened())  // check if we succeeded
            return -1;

    cap.set(CV_CAP_PROP_FRAME_WIDTH,1280);
    cap.set(CV_CAP_PROP_FRAME_HEIGHT,720);

    // Optionally specify the top N possible plates to return (with confidences).  Default is 10
    openalpr.setTopN(1);

    //regional pattern.
    openalpr.setDefaultRegion("tr");


    // Make sure the library loaded before continuing.
    // For example, it could fail if the config/runtime_data is not found
    if (openalpr.isLoaded() == false)
    {
        std::cerr << "Error loading OpenALPR" << std::endl;
        return 1;
    }





    while(true){

    cap >> image;
   //image = imread( argv[1], 1 );

    if(! image.data )                              // Check for invalid input
    {
        cout <<  "Could not open or find the image" << std::endl ;
        return -1;
    }

    //tm.reset(); tm.start();

    // Recognize an image file.  You could alternatively provide the image bytes in-memory.


    //detailEnhance(image, image, 1 , 1);

    //pencilSketch(image, image2, image, 60, 0.07f, 0.02f);

    //Size dsize(0,0);  //fixed image size , if zero just multiply the input image with dx and dy
    //resize(image,image,dsize,2/*dx*/,2/*dy*/,INTER_CUBIC);  //make the image dx and dy times bigger

    //unsharpMask(image);

    //detailEnhance(image, image, 1 , 1);

    //frame.convertTo(frame, -1, contrast,brightness);
    //image.convertTo(image, -1, 1.3,10);

    //image=adjust_HSV(image,15);

    image=laplacian_sharp(image);

    alpr::AlprResults results = openalpr.recognize(image.data, image.elemSize(), image.cols, image.rows, regionsOfInterest);

    //tm.stop();

    // Iterate through the results.  There may be multiple plates in an image,
    // and each plate returns the top N candidates.
    for (int i = 0; i < results.plates.size(); i++)
    {
        alpr::AlprPlateResult plate = results.plates[i];
        std::cout << "plate" << i << ": " << plate.topNPlates.size() << " results" << std::endl;

        for (int k = 0; k < plate.topNPlates.size(); k++)
        {
            alpr::AlprPlate candidate = plate.topNPlates[k];
            std::cout << "    - " << candidate.characters << "\t confidence: " << candidate.overall_confidence;
            std::cout << "\t pattern_match: " << candidate.matches_template << std::endl;
        }
    }

    //std::cout << std::endl << "Plate Recognition Time : " << tm.getTimeMilli() << std::endl;

    //imshow( "Display window", image );                   // Show our image inside it.
    //waitKey(1);                                          // Wait for a keystroke in the window

    }


    return 0;
}
