// Modern .NET, Chapter 4, Section 4.7
int[] array = [1, 2, 3];
List<string> list = ["alpha", "beta"];
IReadOnlyList<int> readOnly = [10, 20, 30];
Span<int> span = [4, 5, 6];
HashSet<char> set = ['a', 'b'];

int[] empty = [];                     // no allocation for the empty array

// The spread operator combines collections
int[] first = [1, 2];
int[] second = [3, 4];
int[] combined = [0, ..first, ..second, 5];   // 0,1,2,3,4,5

Console.WriteLine(string.Join(", ", combined));
