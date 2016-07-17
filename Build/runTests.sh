#!/bin/sh

xbuild IchigoAI.Test/IchigoAI.Test.csproj /property:Configuration=Debug /verbosity:minimal
if [ $? != 0 ]
then
	echo "ERROR: Could not compile!"
	exit 1
fi

mono Deps/NSpec/NSpecRunner.exe IchigoAI.Test/bin/Debug/IchigoAI.Test.dll
