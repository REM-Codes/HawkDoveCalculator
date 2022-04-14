using System;
using System.Collections.Generic;

class Program {
  /// <summary>
  /// The constants used to define the arbitrary payoffs.
  /// </summary>
  /// <value name="WinPayoff">how much an entity earns (can be negative) by winning a fight</value>
  /// <value name="LosePayoff">how much an entity earns (can be negative) by losing a fight</value>
  /// <value name="InjuryPayoff">how much an entity earns (can be negative) by getting injured</value>
  /// <value name="WasteTimePayoff">how much an entity earns (can be negative) by wasting time</value>
  private const double WinPayoff = 50d;
  private const double LosePayoff = 0d;
  private const double InjuryPayoff = -100d;
  private const double WasteTimePayoff = -10d;

  /// <summary>
  /// The number of iterations to run in a simulation.
  /// </summary>
  private const int Iterations = 500;

  /// <summary>
  /// For the computer to keep track of how many entities of each type there are.
  /// </summary>
  /// <value name="DoveCount">how many doves there are</value>
  /// <value name="HawkCount">how many hawks there are</value>
  private static int DoveCount;
  private static int HawkCount;

  /// <summary>
  /// Enum for the types of entities.
  /// </summary>
  public enum Type {
    HAWK,
    DOVE
  }

  /// <summary>
  /// The main program. It employs a for loop that loops according to the number of defined in the <c>Iterations</c> property.
  /// </summary>
  public static void Main(string[] args) {
    for(int k = 0; k < Iterations; k++) {
      RunGeneration();
      int hawk = SimplifyFraction(new int[] {HawkCount, DoveCount})[0];
      int dove = SimplifyFraction(new int[] {HawkCount, DoveCount})[1];
      
      Console.WriteLine(string.Format("Hawk to dove ratio is {0}:{1}.", hawk, dove));
    }
  }

  /// <summary>
  /// Code to run a single generation and add the according entity.
  /// </summary>
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

  /// <summary>
  /// Add a member of certain type, hawk or dove.
  /// </summary>
  /// <param name="t">the type of member to add</param>
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

  /// <summary>
  /// Calculate the average payoff for a hawk.
  /// </summary>
  /// <returns>
  /// a double containing the average payoff for a hawk, according to the predefined payoffs.
  /// </returns>
  private static double AverageHawkPayoff() {
    double result = 0d;
    // Hawk vs Dove
    result += (WinPayoff)*DoveCount;
    // Hawk vs Hawk
    result += CalculateAverage(new double[] {50d, -100d})*HawkCount;
    return result/(DoveCount+HawkCount);
  }

  /// <summary>
  /// Calculate the average payoff for a dove.
  /// </summary>
  /// <returns>
  /// a double containing the average payoff for a dove, according to the payofsf defined at the top
  /// </returns>
  private static double AverageDovePayoff() {
    double result = 0d;
    // Dove vs Dove
    result += CalculateAverage(new double[] {(WinPayoff + WasteTimePayoff), (LosePayoff + WasteTimePayoff)})*DoveCount;
    // Dove vs Hawk
    result += LosePayoff*HawkCount;
    return result/(DoveCount+HawkCount);
  }

  /// <summary>
  /// Util for calculating the average of a set of numbers.
  /// </summary>
  /// <param name="numbers">array of the numbers to calculate the average from</param>
  /// <returns>
  /// a double containing the average of all numbers in the passed array
  /// </returns>
  private static double CalculateAverage(double[] numbers) {
    double sum = 0d;
    foreach(double num in numbers) {
      sum += num;
    }
    return sum/numbers.Length;
  }

  /// <summary>
  /// A util for simplifying a fraction.
  /// </summary>
  /// <param name="fraction">an array contining a fraction with the numerator at index 0 and denominator at index 1</param>
  /// <returns>
  /// integer array containing the simplified fraction with the numerator at index <c>0</c> and denominator at index <c>1</c>
  /// </returns>
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