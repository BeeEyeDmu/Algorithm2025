
namespace _13_Sorting
{
  internal class Program
  {
    static int N = 100;
    static int[] a = new int[100];

    static void Main(string[] args)
    {
      RandomInit();
      PrintArray();

      Console.WriteLine("\nArray.Sort()");
      Array.Sort(a);
      PrintArray();

      Console.WriteLine("\nBubble 정렬");
      RandomInit();
      BubbleSort();
      PrintArray();

      Console.WriteLine("\n선택 정렬");
      RandomInit();
      SelectionSort();
      PrintArray();

      Console.WriteLine("\n삽입 정렬");
      RandomInit();
      InsertionSort();
      PrintArray();

      Console.WriteLine("\n쉘정렬");
      RandomInit();
      ShellSort();
      PrintArray();

      Console.WriteLine("\n힙정렬");
      RandomInit();
      HeapSort();
      PrintArray();

      Console.WriteLine("\n기수정렬(radix sort)");
      RandomInit();
      RadixSort();
      PrintArray();

    }

    private static void RadixSort()
    {
      int max = GetMax();

      // 자리수에 따라 CountingSort를 호출
      for(int exp = 1; max / exp > 0; exp*=10)
        CountingSort(a, exp);
    }

    private static void CountingSort(int[] a, int exp)
    {
      int[] count = new int[10];
      int[] output = new int[N];

      // count[] 계산
      for (int i = 0; i < N; i++)
        count[(a[i] / exp) % 10]++;

      // count[] 누적
      for (int i = 1; i < 10; i++)
        count[i] += count[i - 1];

      // a[]을 뒤에서부터 해당 자리수에 저장
      for(int i=N-1; i >= 0; i--)
      {
        int pos = count[(a[i]/exp) % 10] - 1;  // 배열 인덱스
        output[pos] = a[i];
        count[(a[i] / exp) % 10]--;
      }

      // output[]을 a[]로 복사
      for (int i = 0; i < N; i++)
        a[i] = output[i];
    }

    private static int GetMax()
    {
      int max = a[0];

      for (int i = 1; i < N; i++)
        if (a[i] > max)
          max = a[i];

      return max;
    }

    private static void HeapSort()
    {
      // 힙 자료구조를 만든다
      for (int i = N / 2 - 1; i >= 0; i--)
        DownHeap(a, N, i);

      for(int i=N-1; i>=0; i--)
      {
        swap(0, i);
        DownHeap(a, i, 0);
      }
    }

    private static void DownHeap(int[] a, int n, int i)
    {
      int largest = i;
      int left = 2 * i + 1;
      int right = 2 * i + 2;

      if (left < n && a[left] > a[largest])
        largest = left;
      if (right < n && a[right] > a[largest])
        largest = right;

      if(largest != i)
      {
        swap(i, largest);
        DownHeap(a, n, largest);
      }
    }

    private static void ShellSort()
    {
      int[] h = { 1, 4, 10, 23, 57, 132, 301, 701, 1750 };
      int index = 0;

      while (h[index] < N / 2)
        index++;

      int gap = h[--index];

      while (gap > 0)
      {
        Console.WriteLine("gap = " + gap);

        for (int i = gap; i < N; i++)
        {
          int current = a[i];
          int j = i;
          while (j >= gap && a[j - gap] > current)
          {
            a[j] = a[j - gap];
            j = j - gap;
          }
          a[j] = current;
        }
        //PrintArray();

        if (index == 0)
          break;
        gap = h[--index];
      }
    }

    private static void InsertionSort()
    {
      //    1. for i = 1 to n - 1 {
      //    2.    CurrentElement = A[i] // 정렬 안된 부분의 가장 왼쪽원소
      //    3.    j ← i – 1   // 정렬된 부분의 가장 오른쪽 원소로부터 왼쪽  방향으로 삽입할 곳을 탐색하기 위하여 
      //    4.    while (j >= 0) and(A[j] > CurrentElement) {
      //    5.        A[j + 1] = A[j]   // 자리 이동
      //    6.        j ← j - 1
      //          }
      //    7.    A[j + 1] ← CurrentElement
      //       }
      //    8. return A

      for(int i=1; i<N; i++)
      {
        int cur = a[i];
        int j;

        for(j=i-1; j >= 0 && a[j] > cur; j--)
        {
          a[j + 1] = a[j];
        }
        a[j + 1] = cur;

        //int j = i - 1;

        //while(j >= 0 && a[j] > cur)
        //{
        //  a[j + 1] = a[j];
        //  j = j - 1;
        //}
        //a[j + 1] = cur;
      }
    }

    private static void SelectionSort()
    {
      // 1. for i = 0 to n - 2 {
      // 2.   min = i
      // 3.   for j = i + 1 to n - 1 {    // A[i]~A[n-1]에서 최솟값을 찾는다.
      // 4.     if (A[j] < A[min])
      // 5.        min = j
      //      }
      // 6.   A[i] ↔ A[min]    // min이 최솟값이 있는 원소의 인덱스
      //    }
      // 7. return 배열 A
      for(int i=0; i<N-1; i++)
      {
        int min = i;

        for(int j=i+1; j<N; j++)
          if(a[j] < a[min])
            min = j;

        swap(i, min);
      }

    }

    private static void BubbleSort()
    {
      //1. for pass = 1 to n - 1
      //2.   for i = 1 to n - pass
      //3.     if (A[i - 1] > A[i])  // 위의 원소가 아래의 원소보다 크면
      //4.        A[i - 1] ↔ A[i]   // 서로 자리를 바꾼다.
      //5. return 배열 A

      //for (int pass = 1; pass <= N - 1; pass++)
      //  for (int i = 1; i <= N - pass; i++)
      //    if (a[i - 1] > a[i])
      //      swap(i-1, i);

      for (int i = 1; i <= N - 1; i++)
        for (int j = 1; j <= N - i; j++)
          if (a[j - 1] > a[j])
            swap(j - 1, j);
    }

    private static void swap(int i, int j)
    {
      int tmp = a[i];
      a[i] = a[j];
      a[j] = tmp;
    }

    private static void PrintArray()
    {
      for (int i = 0; i < N; i++)
      {
        Console.Write(a[i] + " ");
      }
      Console.WriteLine();
    }

    private static void RandomInit()
    {
      Random r = new Random();

      for (int i = 0; i < N; i++)
      {
        a[i] = r.Next(1000);
      }
    }
  }
}
