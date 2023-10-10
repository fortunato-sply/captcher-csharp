using System;
using System.Collections.Generic;

public static class Captcher
{
  public static bool verifySpeedText(List<UserData> data)
  {
    int count = 0;
    int events = 0;
    for (int i = 1; i < data.Count; i++)
    {
      string text = data[i].Text;
      bool isEqualText = text == data[i - 1].Text;

      if (isEqualText)
      {
        count++;
      }
      else
      {
        if (count < 1)
        {
          events++;
        }

        count = 0;
      }
    }

    if (events > 3)
    {
      return true; // true: possível cracker
    }

    return false;
  }

  public static bool verifyMovementPatterns(List<UserData> data)
  {
    int events = 0, eventEqualDistance = 0;
    int seq = 0;

    int eventsTreshold = 2;
    int eventEqualDistanceTreshold = 10;

    int distanceX = 0;
    int distanceY = 0;
    for (int i = 1; i < data.Count; i++)
    {
      int x = data[i].X;
      int y = data[i].Y;

      int prevX = data[i - 1].X;
      int prevY = data[i - 1].Y;

      if (x == prevX && y == prevY)
        continue;

      if (x == prevX && y != prevY)
      {
        // if(Math.Abs(y - prevY) == distanceY)
        //   eventEqualDistance++;

        distanceY = Math.Abs(y - prevY);
        if (distanceY > 1)
          seq++;
      }
      else if (y == prevY && x != prevX)
      {
        // if(Math.Abs(x - prevX) == distanceX)
        //   eventEqualDistance++;

        distanceX = Math.Abs(x - prevX);
        if (distanceX > 1)
          seq++;
      }
      else
      {
        seq = 0;

        if (Math.Abs(y - prevY) == distanceY && Math.Abs(x - prevX) == distanceX)
        {
          Console.WriteLine($"X: {x} - Y: {y}");
          eventEqualDistance++;
        }

        distanceX = Math.Abs(x - prevX);
        distanceY = Math.Abs(y - prevY);
      }

      if (seq > 3)
      {
        events++;
        seq = 0;
      }

    }

    Console.WriteLine("EVENTS: " + events);
    Console.WriteLine("event distance: " + eventEqualDistance);
    if (events >= eventsTreshold || eventEqualDistance >= eventEqualDistanceTreshold)
      return true;

    return false;
  }

  public static bool verifyShift(List<UserData> data)
  {
    int numberOfCharacthers = 0;
    int numberOfShifts = 0;
    bool flag = true;

    for (int i = 1; i < data.Count; i++)
    {
      String text = data[i].Text;

      String prevText = data[i - 1].Text;

      if (text == "@" || text == "!" || text == "#" || text == "$" ||
        text == "%" || text == "¨" || text == "&" || text == "*" ||
        text == "(" || text == ")" || text == "{" || text == "}" ||
        text == "`" || text == "^")
      {
        if (flag || text != prevText)
        {
          flag = false;
          if (prevText == "Shift")
          {
            numberOfShifts++;
          }
          numberOfCharacthers++;
        }
      }
      else
      {
        flag = true;
      }
    }

    if (numberOfCharacthers == numberOfShifts)
      return false;
    return true;
  }

  public static bool verifyEnter(List<UserData> data)
  {
    bool flag = false;

    for (int i = 1; i < data.Count; i++)
    {
      string text = data[i].Text;
      if (text == "Enter")
        flag = true;
    }

    if (flag)
      return false;
    return true;
  }

}