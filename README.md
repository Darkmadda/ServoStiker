# ServoStiker
ServoStik joystick launchbox plugin

This launchbox plugin enables the Ultimarc ServoStik(https://www.ultimarc.com/arcade-controls/joysticks/servostik/) joystick to change orientation when a rom is loaded and unloaded based on controller support metadata. This requires JoyTray.exe to work as it does the heavy lifting.

The plugin also relies on proper metadata to determine which mode to set when loading a game. It looks at the controller support metadata. By default it uses the joysticks "8-Way Stick" and "4-Way Joystick" but this can be changed in the conf file.

The plugin should be installed in the launchbox/plugins folder. Copy both the dll and conf files to this folder.

Config options include:
```
	"default":"8-way", // default mode used when joystick isn't set or matched, and when game is exited and set to reset-on-exit
	"8-way-name": "8-Way Joystick,Double 8-Way Joysticks,Half 8-Way Joystick,Atari Joystick,16-Way Joystick",// the name of the controllers(comma separated) in the controller support metadata
	"4-way-name": "4-Way Joystick,Double 4-Way Joysticks,Half 4-Way Joystick,Double Horizontal Joysticks,Double Vertical Joysticks,Horizontal Joystick,Vertical Joystick",// the name of the controllers(comma separated) in the controller support metadata
	"reset-on-exit": "True", // should the joystick be reset to the default when a game is exited
	"joytrayPath": "C:\Program Files (x86)\JoyTray\JoyTray.exe" // the location of installed JoyTray.exe
  ```
