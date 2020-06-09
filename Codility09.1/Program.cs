/*
A non-empty array A consisting of N integers is given.

A triplet (X, Y, Z), such that 0 ≤ X < Y < Z < N, is called a double slice.

The sum of double slice (X, Y, Z) is the total of A[X + 1] + A[X + 2] + ... + A[Y − 1] + A[Y + 1] + A[Y + 2] + ... + A[Z − 1].

For example, array A such that:

    A[0] = 3
    A[1] = 2
    A[2] = 6
    A[3] = -1
    A[4] = 4
    A[5] = 5
    A[6] = -1
    A[7] = 2
contains the following example double slices:

double slice (0, 3, 6), sum is 2 + 6 + 4 + 5 = 17,
double slice (0, 3, 7), sum is 2 + 6 + 4 + 5 − 1 = 16,
double slice (3, 4, 5), sum is 0.
The goal is to find the maximal sum of any double slice.

Write a function:

class Solution { public int solution(int[] A); }

that, given a non-empty array A consisting of N integers, returns the maximal sum of any double slice.

For example, given:

    A[0] = 3
    A[1] = 2
    A[2] = 6
    A[3] = -1
    A[4] = 4
    A[5] = 5
    A[6] = -1
    A[7] = 2
the function should return 17, because no double slice of array A has a sum of greater than 17.

Write an efficient algorithm for the following assumptions:

N is an integer within the range [3..100,000];
each element of array A is an integer within the range [−10,000..10,000].
*/

using System;

namespace Codility09._1
{
    class Solution
    {
        public int solution(int[] A)
        {
            if (A.Length == 3)
                return 0;
            int maxEnding = 0;
            int maxSliceValue = 0;
            int maxSliceEnd = 0;
            for (int i = 1; i < A.Length - 1; i++)
            {
                if (maxEnding + A[i] > 0)
                    maxEnding += A[i];
                else
                    maxEnding = 0;
                if (maxEnding > maxSliceValue)
                {
                    maxSliceValue = maxEnding;
                    maxSliceEnd = i;
                }
            }
            int sum = 0;
            int minInSlice = int.MaxValue;
            for (int i = maxSliceEnd; i > 0; i--)
                sum += A[i];
            int maxSliceStart = 1;
            while (sum != maxSliceValue)
            {
                sum -= A[maxSliceStart];
                maxSliceStart++;
            }
            int rightSlice = int.MinValue;
            int leftSlice = int.MinValue;
            if (maxSliceEnd < A.Length - 2)
            {
                int pos = maxSliceEnd + 2;
                int tempRightSlice = 0;
                while (pos < A.Length - 1)
                {
                    tempRightSlice += A[pos];
                    if (tempRightSlice > rightSlice)
                        rightSlice = tempRightSlice;
                    pos++;
                }
            }
            if (maxSliceStart > 1)
            {
                int pos = maxSliceStart - 2;
                int tempLeftSlice = 0;
                while (pos > 1)
                {
                    tempLeftSlice += A[pos];
                    if (tempLeftSlice > leftSlice)
                        leftSlice = tempLeftSlice;
                    pos--;
                }
            }
            for (int i = maxSliceStart; i <= maxSliceEnd; i++)
                if (A[i] < minInSlice)
                    minInSlice = A[i];
            int res = maxSliceValue - minInSlice;
            if ((maxSliceEnd < A.Length - 2 || maxSliceStart > 1) && res < maxSliceValue)
                res = maxSliceValue;
            if (maxSliceValue + rightSlice > res)
                res = maxSliceValue + rightSlice;
            if (maxSliceValue + leftSlice > res)
                res = maxSliceValue + leftSlice;
            return res;
        }
    }

    class Program
    {
        static void Main()
        {
            Solution sol = new Solution();
            int[] A = { 3, 2, 6, -1, 4, 5, -1, 2 };
            //int[] A = { -2, -3, -4, 1, -5, -6, -7 };
            //int[] A = { 3, 2, 6, -1, 4, 5, -20, 9, 1 };
            //int[] A = { 6, 1, 5, 6, 4, 2, 9, 4 };
            //int[] A = { 0, 10, -5, -2, 0 };
            //int[] A = { 5, 17, 0, 3 };
            //int[] A = { -8, 10, 20, -5, -7, -4 };
            //int[] A = new int[300];
            //Random r = new Random();
            //for (int i = 0; i < 300; i++)
            //    A[i] = r.Next(61) - 30;
            int s = sol.solution(A);
            Console.WriteLine("Solution: " + s);
            //Console.WriteLine("Solution: " + string.Join(", ", s));
        }
    }
}
