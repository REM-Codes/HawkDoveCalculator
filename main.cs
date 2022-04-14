using System;
using System.Collections.Generic;

class Program {
  private double WinPayoff = 50d;
  private double LosePayoff = 0d;
  private double InjuryPayoff = -100d;
  private double WasteTimePayoff = -10d;
  private int Iterations = 500;
  
  private static int DoveCount;
  private static int HawkCount;

  public enum Type {
    HAWK,
    DOVE
  }
  
  public static void Main(string[] args) {
    for(int k = 0; k < iterations; k++) {
      RunGeneration();
      int hawk = SimplifyFraction(new int[] {HawkCount, DoveCount})[0];
      int dove = SimplifyFraction(new int[] {HawkCount, DoveCount})[1];
      
      Console.WriteLine(string.Format("Hawk to dove ratio is {0}:{1}.", hawk, dove));
    }
  }

  private static void RunGeneration() {
    if(AverageHawkPayoff() > AverageDovePayoff()) {
      AddMember(Type.HAWK);
    } else if(AverageHawkPayoff() < AverageDovePayoff()) {
      AddMember(Type.DOVE);
    } else {
      AddMember(Type.HAWK);
      AddMember(Type.DOVE);
    }
  }

  private static void AddMember(Type t) {
    switch(t) {
      case Type.HAWK:
        HawkCount++;
        break;
      case Type.DOVE:
        DoveCount++;
        break;
    }
  }
  
  private static double AverageHawkPayoff() {
    double result = 0d;
    // Hawk vs Dove
    result += (winPayoff)*DoveCount;
    // Hawk vs Hawk
    result += CalculateAverage(new double[] {50d, -100d})*HawkCount;
    return result/(DoveCount+HawkCount);
  }

  private static double AverageDovePayoff() {
    double result = 0d;
    // Dove vs Dove
    result += CalculateAverage(new double[] {(WinPayoff + WasteTimePayoff), (LosePayoff + WasteTimePayoff)})*DoveCount;
    // Dove vs Hawk
    result += LosePayoff*HawkCount;
    return result/(DoveCount+HawkCount);
  }
  
  private static double CalculateAverage(double[] numbers) {
    double sum = 0d;
    foreach(double num in numbers) {
      sum += num;
    }
    return sum/numbers.Length;
  }

  private static int[] SimplifyFraction(int[] fraction) {
    List<int> num1Factors = new List<int>();
    List<int> num2Factors = new List<int>();
    List<int> commonFactors = new List<int>();
    int result = 1;
    for(int j = 1; j <= fraction[0]; j++) {
      if(fraction[0]%j == 0) {
        num1Factors.Add(j);
      }
    }
    for(int j = 1; j <= fraction[1]; j++) {
      if(fraction[1]%j == 0) {
        num2Factors.Add(j);
      }
    }
    foreach(int i in num1Factors) {
      if(num2Factors.Contains(i)) {
        commonFactors.Add(i);
      }
    }
    foreach(int i in commonFactors) {
      if(i > result) {
        result = i;
      }
    }
    return new int[] {fraction[0]/result, fraction[1]/result};
  }
}