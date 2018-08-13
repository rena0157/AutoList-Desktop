# AutoCADLI - The AutoCAD List Extraction Tool

This is an application that is used to extract AutoCAD Entity information from a list 
command's output. The list Command generates an output text that is then copied into the 
Applications text box and then information is extracted.

![](assets/AutoCADLIsttoolimage.png)

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

where the above is the output when the user has selected a LWPOLYLINE. CADLI can then use this text to extract the length of
the LWPOLYLINE. 

## Currently Supported Objects

- LWPOLYLINEs "Polylines": Length property extraction is currently supported
- LINEs regular lines: Length property extraction is currently supported
- ARCS: Regular arcs are now good for extraction
- HATCHs: area property extraction is currently supported

### Blocks and group lists
Blocks are groups of lines/polylines and hatches that can be read from the text and placed into a CSV file. 
This is useful if you would like to get both the area and the perimeter of a "Block". I am currently using this in
residential subdivision design where my polyline is the frontage and the hatch is the area of the block.

## Future Development