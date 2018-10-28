# AutoCADLI - The AutoCAD List Extraction Tool

This is an application that is used to extract AutoCAD Entity information from a list 
command's output. The list Command generates an output text that is then copied into the 
Applications text box and then information is extracted. Also I now have an online version avalible at
[AutoList Online](https://adamssite.azurewebsites.net/Home/AutoList). 

![](https://github.com/rena0157/AutoList-Desktop/blob/master/assets/AutoCADLIsttoolimage.PNG)

## Introduction

In AutoCAD there is a command that is called `LIST` or simply `LI` its output generall looks like this

```
Select objects:
                  LWPOLYLINE  Layer: "0"
                            Space: Model space
                   Handle = 240
              Open
    Constant width    0.0000
              area   76.9729
            length   30.9270

          at point  X= 107.1441  Y=  46.3544  Z=   0.0000
             bulge   -0.2599
            center  X= 136.6538  Y=  39.0116  Z=   0.0000
            radius   30.4094
       start angle       166
         end angle       108
          at point  X= 127.3799  Y=  67.9725  Z=   0.0000

```

where the above is the output when the user has selected a LWPOLYLINE. AutoList can then use this text to extract the length of
the LWPOLYLINE.

## Currently Supported Objects
- MTEXT: Mtext objects for block names
- TEXT: Text objects for block names
- LWPOLYLINEs "Polylines": Length property extraction is currently supported
- HATCHs: area property extraction is currently supported

### For Block Extraction
There is an option for block extraction. A block is something that is used in residential development planning. On a 
block there is a frontage line represented as either a line or a polyline and the area of the block is represented using a 
hatch. Also, the blocks name is represented using text. The data must be selected in the order of text, frontage and then
area, a frontage line is optional. This will then output as a csv file.