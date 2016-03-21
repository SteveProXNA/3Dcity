REM   --3D City--
REM StevePro Studios
REM 17-March-2016
REM
REM Use this batch script to build source code!

cp tile/*_tile* .
cp engine/*_manager* .
cp screen/*_screen* .

make

del *_screen*
del *_manager*
del *_tile*