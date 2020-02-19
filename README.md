# SafeAddress
SafeAddress is a VSTO AddIn for Microsoft Outlook 2013 or newer.
It's purpose is to provide a visual warning in form of a small hint when it finds "unsafe" address among the recipients.
Safe addresses are resolved based on host (e.g. everything after @)

<img src="https://thumbs.gfycat.com/HonoredIdealisticAplomadofalcon-small.gif"></img>

There are two ways of modifying safe hosts:
* Change property under key "safeDomain" in SafeAddress.dll.config file located in installation directory.
* Add hosts using from within Outlook application in SafeAddres AddIn Settings located inside Safe Address ribbon.

<img src="https://i.imgur.com/VbIM9Bx.png"></img>

Hosts loaded from SafeAddress.dll.config file cannot be modified from SafeAddress Settings.



