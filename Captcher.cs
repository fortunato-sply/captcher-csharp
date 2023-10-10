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

  public static bool verifyMovementPatterns(List<UserData> data) {
    int events = 0;
    int seq = 0;

    int distanceX;
    int distanceY;
    for(int i = 1; i < data.Count; i++) {
      int x = data[i].X;
      int y = data[i].Y;

      int prevX = data[i-1].X;
      int prevY = data[i-1].Y;

      if(x == prevX && y == prevY)
        continue;
      
      if(x == prevX && y != prevY) {
        distanceY = Math.Abs(y - prevY);
        if(distanceY > 1)
          seq++;
      }
      else if (y == prevY && x != prevX) {
        distanceX = Math.Abs(x - prevX);
        if(distanceX > 1)
          seq++;
      }
      else {
        seq = 0;
      }
      
      if(seq > 3) {
        events++;
        seq = 0;
      }

    }

    if(events >= 5)
      return true;

    return false;
  }

}