I guess everyone knows how Lenovo removed the HDD LED on their recent ThinkPad models (it is back on the new ThinkPad P50 and ThinkPad P70). Buying a new computer just to get that option back, which for some people is useful (tbh, it does not bother me - I just did as an exercise for the recent things I've been learning about, plus that I am looking into other ways of exploiting the LEDs, i.e. displaying other information). Today, I am introducing the ThinkPad LEDs Control application, which allows you to:

    Individually turn on/off, or into the third state (which is usually just blink) four LEDs present on current ThinkPads: power, the back red dot (on the i from ThinkPad), microphone mute LED, and sleep (this one does not exist on the W540/W541 I have, but it may exist on other models)
    Turn each of the LEDs on/off separately for read, respectively write operations on the disks. This way, each of the LEDs can become a R/W LED for your ThinkPad; or you can use 2 LEDs for this function: one for R, one for W - the choice is yours
    Toggle LED states from the command line; the available commands are: ** minimize - starts the application minimized ** exit - imediatly terminates the application ** on - turns on the LED corresponding to the previous word, which should be LEDPower, LEDRedDot, LEDMicrophone, LEDSleep ** off - same as on, but turns them off ** third - same as on, but turns them to third state (usually blink)
    Ability to show a virtual LED icon in the notification area, which distinctevly highlights R/W/RW/none operations on the disk(s).
    Monitor changes to Caps Lock and/or NumLock keys, and act accordingly
    Automatically start at system startup

An example for calling the application from command line could be: LEDControl.exe minimize LEDPower off LEDMicrophone third exit - the application will start minimized, toggle the power LED off, make the microphone LED blink, and then terminates.

The application is full OSS (Open Source Software). It uses the TVicPort freeware, code from TPFanControl (C++), and an HDD monitor example from Microsoft (VB.NET). The application is written in C# 5.0. Currently it is compiled against the .NET Framework 4.5.2, but it works starting even from Framework 2.0 I believe, you can recompile it from source and see, I just left it on 4.5.2 as that is the default in Visual Studio 2015. The software is available under ISC license (https://en.wikipedia.org/wiki/ISC_license).

In order to run, please either install TPFanControl, which should install the TVicPort driver, from: http://www.tpfancontrol.com/, either download TVicPort driver from http://entechtaiwan.com/dev/port/index.shtm.

Download here (1.2): https://googledrive.com/host/0BzZ1AE59CpFgVVhLZ2RCeWZ2VE0/LEDControl.zip (including source code, of course, the app is both in root of ZIP, and in LEDControl\LEDControl\bin\x86\Debug)

Old versions: https://googledrive.com/host/0BzZ1AE59CpFgVVhLZ2RCeWZ2VE0/LEDControl_1.0.0.0.zip https://googledrive.com/host/0BzZ1AE59CpFgVVhLZ2RCeWZ2VE0/LEDControl_1.1.0.0.zip

Thanks for checking it out, please report back your opinions, and enjoy!

Edit: New version released, 1.1.0.0:

    Adds support for monitoring changes to Caps Lock key, and toggle LEDs as answer

Edit(2): new version released, 1.2.0.0:

    Improves the way the key changes are detected - now, if the application is run as administrator, the detection will be instantaneous and virtually zero taxing on the CPU; if run normally, the new mechanism will work only for apps that run with the same privileges as this application - for apps run as administrator, the old mechanism (with the delay) will be applied. thus, the delay does nothing if the app is run as admin, can be set to any value, and the same effect is achieved
    Added option to set delay between the disk drives are checked for activity, previously, this was hard coded to 1 ms
    Added option to disable key monitoring altogether, alongside the existing option to disable disk activity monitoring
    Added option to have the launch automatically run at startup in the same context the app is already in (for e.g., if the app was run as administrato, next time you start the system up, the app will run as administrator, without UAC prompt)
    Added posibility of monitoring the NumLock key as well, besides the existing Caps Lock option
    Added new information about resources used, and version info, in the About screen
    Added several new explanaitions for the features offered via the '?' buttons

Edit(3): new version released, 1.3.0.0

    Checks monitored keys when started minimized, so the keys are accurately read from the get-go
    TO-DO: Disable HDD updates when computer prepares to sleep, and resume when computer wakes up, in order to avoid illegal access and writes to the Embedded Controller (EC)
