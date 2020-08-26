# ThinkPad LED Control

ThinkPad LED Control is a Microsoft Windows application that allows controlling the various LEDs present on ThinkPad laptops and linking them to various system events (disk activity, key presses etc.).

## Download

Binaries are available in [Releases](https://github.com/valinet/ThinkPadLEDControl/releases). All other places where these may have been stored (Google Drive, Google Firebase) may be discontinued in the future, and removed at any time, although I will do my best to provide a redirect to this new location when possible.

## Features

Current features of the application are:

* Individually turn on/off, or into the third state (which is usually just blink) four LEDs present on current ThinkPads: power, the back red dot (on the i from ThinkPad), microphone mute LED, and sleep
* Turn each of the LEDs on/off separately for read, respectively write operations on the disks. This way, each of the LEDs can become a R/W LED for your ThinkPad; or you can use 2 LEDs for this function: one for R, one for W
* Toggle LED states from the command line; the available commands are: 
  * minimize - starts the application minimized 
  * exit - imediatly terminates the application 
  * on - turns on the LED corresponding to the previous word, which should be LEDPower, LEDRedDot, LEDMicrophone, LEDSleep 
  * off - same as on, but turns them off 
  * third - same as on, but turns them to third state (usually blink)
* Possibility to show a virtual LED icon in the notification area, which distinctevly highlights R/W/RW/none operations on the disk(s).
* Monitor changes to Caps Lock and/or NumLock keys, and toggle various LEDs
* Start automatically at boot

## Command line

An example for calling the application from command line is: 

```
LEDControl.exe minimize LEDPower off LEDMicrophone third exit
```

The application will start minimized, toggle the power LED off, make the microphone LED blink, and then terminates.

## Driver

In order to change the status of the LEDs, the application needs to interface with the embedded controller on the ThinkPad computers. It does this by using either one of these two kernel drivers: WinRing0 or TVicPort:
* WinRing0 is a more secure choice, because it is an open-source driver which allows only applications that are run as administrator to interface with it. 
* TVicPort is an old, unsecure driver which is popular because it is being used by [TPFanControl](https://thinkwiki.de/TPFanControl), a well-known application that allows controlling the fan speed on ThinkPad computers. Because it allows arbitrary applications to interface with it, it is highly not recommended to use it.

## About

The application is free software. It uses the TVicPort freeware, code from open source TPFanControl (C++), and an HDD monitor example from Microsoft (VB.NET). The application is written in C# 5.0. Currently it is compiled against the .NET Framework 4.5.2, but it works starting even from Framework 2.0 I believe, you can recompile it from source and see, I just left it on 4.5.2 as that is the default in Visual Studio 2015. 

## License

The software is available under ISC license (https://en.wikipedia.org/wiki/ISC_license). The text of the license is available at [LICENSE](https://github.com/valinet/ThinkPadLEDControl/blob/master/LICENSE).

## Changelog

Changelog can be found at [CHANGELOG](https://github.com/valinet/ThinkPadLEDControl/blob/master/CHANGELOG). There is also a [Reddit thread](https://www.reddit.com/r/thinkpad/comments/49wtqw/hdd_led_for_all_thinkpads_hopefully/) where this application has been discussed extensively.
