#sudo apt-get install libgtk-3-dev libavcodec-dev libavdevice-dev libavfilter-dev libavformat-dev libavresample-dev libavutil-dev libgphoto2-dev

#sudo g++ example.cpp -o opencv_example -L /home/pi/TeknoTAM/people_detect/ -lopencv_core -lopencv_imgcodecs -lopencv_imgproc -lopencv_highgui -lopencv_objdetect -lpthread

sudo g++ main.cpp -o plate_image -L /usr/local/lib/ -lopencv_core -lopencv_imgcodecs -lopencv_videoio -lopencv_imgproc -lopencv_highgui -lopencv_objdetect -lpthread -lopenalpr
