xamarin-forms-book-preview
==========================

Sample code for the Preview Edition of "Creating Mobile Apps with Xamarin.Forms"

Notes
-----

All projects have been upgraded to Xamarin.Forms 1.2.3.

###Introduction

In the last sentence under the **Acknowledgements** header (page 29) the word "with" should be "without." (Sorry, Deirdre.)

###Chapter 2

Under the **Say hello** header, in the paragraph that begins "In Visual Studio, you'll probably want to display..." (pg 29), the toolbar identified as **Xamarin.Android** should be just **Android**.

Under the **Frame and BoxView** header, the code sample following the paragraph "Try this" (straddling pgs. 71 and 72) is missing a right curly brace and a semicolon, and it might show up as a right square bracket in some ereaders.

In the *BlackCatPage* and *BlackCatSapPage* classes, in the initializatin of the *ScrollView* (pg. 76) the property *IsClippedToBounds* has been set to *true*. This fixes a scrolling problem encountered on Android devices in Xamarin.Forms 1.2.3. 

###Chapter 3

Under the **Automated data bindings** header, in the bulleted list of *BindingMode* enumeration members (pg. 134), *OneWayToSource* should be described as "target updates source".

###Chapter 5

Under the **Image and bitmaps** header, in the code sample following the paragraph beginning "The *BitmapFromResourcePage* class loads the smaller (pg 218), the filename should be "ModernUserInterface256.JPG".

###Chapter 6

Under the **Custom views: a radio button** header, the code listing of the *RadioButton* class has an incorrect character in the printed copy for the settings of the *checkOff* and *checkOn* variables. The two variables should be initialized as "\u25CB" and "\u25C9".

The *TapGestureRecognizer* in the *RadioButton* class (pg. 254) and *RadioExtensions* class (pg. 256) has been changed from a callback to a *Tapped* handler because the callback is now deprecated.

