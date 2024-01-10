// See https://aka.ms/new-console-template for more information

using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;

const int leftBound = -100;
const int rightBound = 100;

int[] generateArray(int count) {
    Random random = new Random();
    int[] result = new int[count];
    for (int i = 0; i < count; ++i) {
        result[i] = random.Next(leftBound, rightBound);
    }
    return result;
}

void printArray(int[] array) {
    foreach (int el in array) {
        Console.Write(el + " ");
    }
    Console.WriteLine();
}

int[,] generateMatrix(int m, int n) {
    Random random = new Random();
    int[,] result = new int[m,n];
    for (int i = 0; i < m; ++i) {
        for(int j = 0; j < n; ++j) {
            result[i,j] = random.Next(leftBound, rightBound);
        } 
    }
    return result;
}

void printMatrix(int[,] matrix) {
    for (int i = 0; i < matrix.GetLength(0); ++i) {
        for (int j = 0; j < matrix.GetLength(1); ++j) {
            Console.Write($"{matrix[i, j], 5}");
        }
        Console.WriteLine();
    }
}

const int N = 10;

Console.WriteLine("__________FIRST PART__________");
while(true) {
    int[] array = generateArray(N); 
    Console.WriteLine("Original array:");
    printArray(array);

    int minIdx = Array.IndexOf(array, array.Min());
    Console.WriteLine("Index of the min element = " + minIdx);

    int firstNegativeIdx = Array.FindIndex(array, el => el < 0);
    int sum = 0;
    if (firstNegativeIdx != -1) {
        int secondNegativeIdx = Array.FindIndex(array, firstNegativeIdx + 1, el => el < 0);
        if(secondNegativeIdx != -1) {
            for(int i = firstNegativeIdx + 1; i < secondNegativeIdx; ++i) {
                sum += array[i];
            }
        }
    }
    Console.WriteLine("Sum beteween first and second negative =" + sum);

    int[] sorted = new int[10];
    int start = 0;
    int end = 9;
    foreach (int el in array) {
        if (Math.Abs(el) <= 1) {
            sorted[start++] = el;
        } else {
            sorted[end--] = el; 
        }
    }
    array = sorted;
    Console.WriteLine("Modified array:");
    printArray(array);

    Console.WriteLine("\nRepeat? y/n");
    string response = Console.ReadLine();
    if (response != "y") {
        break;
    }
}
Console.WriteLine("__________END OF THE FIRST PART__________");

const int ROWS  = 5;
const int COLS = 5;

Console.WriteLine("__________SECOND PART__________");
while(true) {
    int[,] matrix = generateMatrix(ROWS, COLS);
    Console.WriteLine("Original matrix:");
    printMatrix(matrix);
    
    (int, int)[] matrixValue = new (int, int)[COLS];
    for (int j = 0; j < COLS; ++j) {
        int value = 0;
        for (int i = 0; i < ROWS; ++i) {
            if (matrix[i, j] < 0 && matrix[i, j] % 2 != 0) {
                value += Math.Abs(matrix[i, j]);
            }
        }
        matrixValue[j] = (j , value);
    } 
    int sumOfSelCols = 0;
    foreach (var pair in  matrixValue) {
        if (pair.Item2 != 0) {
            for (int i = 0; i < ROWS; ++i) {
                sumOfSelCols += matrix[i, pair.Item1];
            }
        }
    }
    Console.WriteLine("Sum of elements in rows with negative elems :" + sumOfSelCols);

    matrixValue = matrixValue.OrderBy(pair => pair.Item2).ToArray();
    int[,] modified = new int[ROWS, COLS];
    for (int j = 0; j < COLS; ++j) {
        for(int i = 0; i < ROWS; ++i) {
            modified[i, j] = matrix[i, matrixValue[j].Item1];
        }
    }
    matrix = modified;
    Console.WriteLine("Modified matrix:");
    printMatrix(matrix);

    
    Console.WriteLine("\nRepeat? y/n");
    string response = Console.ReadLine();
    if (response != "y") {
        break;
    }
}
Console.WriteLine("__________END OF THE SECOND PART__________");



