﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

var defaultJsons = new string[]
{
    "user-data 10.json",
    "user-data 16.json",
};
var file = args.Length == 0 || !File.Exists(args[0]) ?
      defaultJsons[Random.Shared.Next(2)] : args[0];
List<UserData> data = UserData.Read(file);

bool isSpeedTextSuspect = Captcher.verifySpeedText(data);
bool isMovementPatternsSuspect = Captcher.verifyMovementPatterns(data);
bool isSpecialKeysSuspect = Captcher.verifyShift(data);
bool isEnterKeySuspect = Captcher.verifyEnter(data);

Console.WriteLine(isSpeedTextSuspect);
Console.WriteLine(isMovementPatternsSuspect);
Console.WriteLine(isSpecialKeysSuspect);
Console.WriteLine(isEnterKeySuspect);

if(!isEnterKeySuspect)
    isUser();
else
    if(isSpeedTextSuspect || isMovementPatternsSuspect || isSpecialKeysSuspect)
        isCracker();
    else
        isUser();

void isCracker()
    => Console.WriteLine("Cracker");

void isUser()
    => Console.WriteLine("User");