//Problems:
//TwoSum
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

static int[] twoSum(int[] nums, int target)
{
    var map = new Dictionary<int, int>();
    int[] result = new int[2];

    for(int i = 0; i < nums.Length; i++)
    {
        //Calculate current target based on current number we're iterating over
        int diff = target - nums[i];
        //If our map contains diff, we have our answer and return; else we add the curr number to map and continue iterating
        if(map.ContainsKey(diff))
        {
            result = new int[] { map[diff], i };
            break;
        }
        else
        {
            map[nums[i]] = i;
        }
    }
    return result;

}
//Group Anagrams
static IList<IList<string>> GroupAnagrams(string[] strs)
{
    var ans = new Dictionary<string, List<string>>();

    foreach (var s in strs)
    {
        //Foreach string, create an integer array to store the string's character frequencies
        var count = new int[26];               
        foreach (var c in s)
        {
            count[c - 'a']++;   //Fill array using ASCII values
        }

        var key = string.Join(',', count); //Convert arr to string so it can be used as a key in out Dictionary
        //Fill the Dictionary
        if (!ans.ContainsKey(key))
        {
            ans[key] = new List<string>();
        }

        ans[key].Add(s);
    }

    //Return out Dictionary's Values, as they store all lists of anagrams
    return new List<IList<string>>(ans.Values);
}

//TopK
static int[] TopK(int[] nums, int k)
{

    Dictionary<int, int> count = new Dictionary<int, int>(); //store each num + freq
    List<int>[] buckets = new List<int>[(nums.Length + 1)]; //create buckets, acccount for case where all nums are same with the +1

    //Instantiate all lists in buckets as empty lists
    for (int i = 0; i < buckets.Length; i++)
    {
        buckets[i] = new List<int>();
    }

    //fill count
    for (int i = 0; i < nums.Length; i++)
    {
        if (!count.ContainsKey(nums[i]))
        {
            count[nums[i]] = 0;
        }
        count[nums[i]]++;
    }

    //fill buckets
    foreach (var kvp in count)
    {
        buckets[kvp.Value].Add(kvp.Key);
    }

    //Create and fill result array
    int[] result = new int[k];
    int index = 0;
    for (int i = buckets.Length - 1; i < 0 && index < k; i--) //Iterate through buckets backwards, as number frequency is 'sorted' by index 
    {
        foreach (int n in buckets[i])
        {
            result[index++] = n;
            if (index == k)
            {
                return result;
            }
        }
    }
    return result;

}

//encode + decode strings
static string encode(List<string> strs)
{
    //Encode using the string's length + a delimiter to avoid cases where the strings contain our delimiter as a character
    return string.Concat(strs.SelectMany(s => $"{s.Length}#{s}")); 
}
static List<string> decode(string s)
{
    //instantiate result and index
    var res = new List<string>();
    var i = 0;
    while (i < s.Length)
    {
        var j = i;
        //Move through string to our delimiter
        while (s[j] != '#')
        {
            j++;
        }

        //Capture curr string's length in len
        int.TryParse(s.Substring(i, j - i), out var len);
        //Move past delimiter
        j++;
        //Add string to our list and update i to next string's encoded length (int before delimiter(#))
        res.Add(s.Substring(j, len));
        i = j + len;
    }
    return res;
}

//Product of Array Except Self (No Division Allowed!)
static int[] ProductExceptSelf(int[] nums)
{

    //Store num's length + create suffix, prefix, and result arrays 
    int n = nums.Length;
    int[] prefix = new int[n];
    int[] suffix = new int[n];
    int[] result = new int[n];

    //Set 1st index of prefix + last index of suffix arrays to 1 
    prefix[0] = 1;
    suffix[n - 1] = 1;

    //Calc prefix and suffix arrays
    for (int i = 1; i < n; i++)
    {
        prefix[i] = nums[i - 1] * prefix[i - 1];
    }

    for (int i = n - 2; i >= 0; i--)
    {
        suffix[i] = nums[i + 1] * suffix[i + 1];
    }

    //Calc and return result array from our prefix and suffix arrays
    for (int i = 0; i < n; i++)
    {
        result[i] = prefix[i] * suffix[i];
    }

    foreach (int num in result)
    {
        Console.WriteLine(num);
    }
    return result;

}

//Longest consecutive sequence
static int LongestConsecutiveSequence(int[] nums)
{
    //Create HashSet from nums + instantiate longest
    HashSet<int> numSet = new HashSet<int>(nums);
    int longest = 0;

    //Iterate over HashSet
    foreach(int num in numSet)
    {
        //Check to see if num is start of a sequence
        if(!numSet.Contains(num - 1))
        {
            int length = 1;
            //While HashSet contains next sequence num, increment length
            while(numSet.Contains(num + length))
            {
                length++;
            }
            longest = Math.Max(longest, length);
        }
    }
    return longest;

}

//Valid Parentheses
static bool IsValidParens(string s)
{
    //Instantiate an empty stack
    var stack = new Stack<char>();

    //Create a Dictionary to store the relationship between closing and opening parentheses
    var pairs = new Dictionary<char, char>()
    {
        [')'] = '(',
        [']'] = '[',
        ['}'] = '{'
    };

    //Iterate through each character in the input string
    foreach (char c in s)
    {

        //If paren is NOT a closing paren, add it to the stack
        if (!pairs.ContainsKey(c))
        {
            stack.Push(c);
        }

        //If paren IS a closing paren, we check for failure conditions:
        //Check if stack is empty (represents no matching open paren, meaning string is invalid)
        //Otherwise, pop and check if the popped char matches the corresponding open char. If not,
        //string is invalid, as opening + closing paren do not match.

        else if (stack.Count == 0 || stack.Pop() != pairs[c])
        {
            return false;
        }
    }

    //Return true if we've emptied the stack, meaning all opening parens had matching closing parens in the correct order.
    //If there are still elements in the stack, it means that there were open parens in the input string that were never closed.

                return stack.Count == 0;
}

static int EvalRPN(string[] tokens)
{
    //Instantiate a stack
    Stack<int> stack = new Stack<int>();

    //Iterate through our input array
    foreach (string c in tokens)
    {
        //Cover all 'operator' cases, making sure - and / are handled in the correct order
        if (c == "+")
        {
            stack.Push(stack.Pop() + stack.Pop());
        }
        else if (c == "-")
        {
            int a = stack.Pop();
            int b = stack.Pop();
            stack.Push(b - a);
        }
        else if (c == "*")
        {
            stack.Push(stack.Pop() * stack.Pop());
        }
        else if (c == "/")
        {
            int a = stack.Pop();
            int b = stack.Pop();
            stack.Push((int)((double)b / a));
        }

        //If not an operator, simply push the num onto the stack
        else
        {
            stack.Push(int.Parse(c));
        }
    }

    //Given that we will always receive valid input, return last number left in stack
    //after iterating through entire input array
    return stack.Pop();
}

static bool IsValidSudoku(char[][] board)
{
    //Create dictionaries for cols, rows, and squares
    Dictionary<int, HashSet<char>> cols = new Dictionary<int, HashSet<char>>();
    Dictionary<int, HashSet<char>> rows = new Dictionary<int, HashSet<char>>();
    Dictionary<int, HashSet<char>> squares = new Dictionary<int, HashSet<char>>();

    //iterate through input board
    for (int r = 0; r < 9; r++)
    {
        for (int c = 0; c < 9; c++)
        {
            char cell = board[r][c];
            //skip empty cells
            if (cell == '.')
            {                                                                                               
                continue;
            }
            //Check for dupes; return false if found
            if (rows.TryGetValue(r, out var rowSet) && rowSet.Contains(cell)                                               
                    || cols.TryGetValue(c, out var colSet) && colSet.Contains(cell)
                    || squares.TryGetValue((r / 3) * 3 + c / 3, out var squareSet) && squareSet.Contains(cell))
            {
                return false;
            }
            //These three lines will add a new row, col, or square if need be
            cols.TryAdd(c, new HashSet<char>());                                                                           
            rows.TryAdd(r, new HashSet<char>());
            squares.TryAdd((r / 3) * 3 + c / 3, new HashSet<char>());
            //These three lines will add the characters to the row/col/square's HashSet
            cols[c].Add(cell);                                                                                              
            rows[r].Add(cell);
            squares[(r / 3) * 3 + c / 3].Add(cell);
        }
    }
    //Return true if we're able to iterate over the whole input board
    return true;
}

//Generate Valid Parens
public class Solution
{
    //Create an entry point to our recursive function
    public IList<string> GenerateParenthesis(int n)
    {
        List<string> result = new List<string>();
        Recurse(n, 0, 0, "", result);
        return result;
    }

    //Create our recursive function to build valid paren strings
    private void Recurse(int n, int openP, int closedP, string curr, List<string> result)
    {
        //Base case: add curr string to result when open parens == closed parens == n and return
        if (openP == closedP && openP == n)
        {
            result.Add(curr);
            return;
        }

        //If open parens are less than n, recurse with openP + 1 and curr + "("
        if (openP < n)
        {
            Recurse(n, openP + 1, closedP, curr + "(", result);
        }

        //If closed parens are less than open parens, recurse with closeP + 1 and curr + ")"
        if (closedP < openP)
        {
            Recurse(n, openP, closedP + 1, curr + ")", result);
        }
    }

    static int[] DailyTemperatures(int[] temperatures)
    {

        //Instantiate result array + stack
        int[] res = new int[temperatures.Length];
        Stack<int[]> stack = new Stack<int[]>();

        for (int i = 0; i < temperatures.Length; i++)
        {
            int t = temperatures[i];
            //Pop from stack when following num in temperatures array is greater than previous num (num at top of stack)
            //Then push the difference in their indexes to result array
            while (stack.Count > 0 && t > stack.Peek()[0])
            {
                int[] pair = stack.Pop();
                res[pair[1]] = i - pair[1];
            }
            //If stack is empty or number we're looking at is <= stack.Peek(), push to stack
            stack.Push(new int[] { t, i });
        }
        return res;
    }

    static int CarFleet(int[] position, int[] speed, int target)
    {
        //n = input arrays len
        int n = position.Length;

        //Create jagged array of double arrays... each ele is an array of doubles. Jagged because inner arrs
        //can have different lengths. Pairs initializes the outer array object (of len(n)) and leaves all
        //inner arrs uninitialized 
        double[][] pairs = new double[n][];

        //iterate through input arrays and add each car to pairs: [pos, speed]
        for (int i = 0; i < n; i++)
        {
            pairs[i] = new double[] { position[i], speed[i] };
        }

        //Sort our pairs object via position (arr[0]); result will be ordered by descending order of position
        Array.Sort(pairs, (a, b) => b[0].CompareTo(a[0]));

        int fleetCount = 0;

        //Initialize our timeToReach double array
        double[] timeToReach = new double[n];

        //Iterate over our now sorted jagged pairs array to fill timeToReach
        for (int i = 0; i < n; i++)
        {
            //time to reach target = target - (car's position / speed)
            timeToReach[i] = (target - pairs[i][0]) / pairs[i][1];

            //If car is not first car in arr (positioned closest to target in our set of cars) and
            //timeToReach for car is less than or equal to car ahead([i-1]), set timeToReach of 
            //curr car ([i]) = car ahead (because this car will catch car ahead (or meet at destination)
            //and match its speed, joining the ALREADY CREATED (via below else statement) car fleet at this point).
            if (i >= 1 && timeToReach[i] <= timeToReach[i - 1])
            {
                timeToReach[i] = timeToReach[i - 1];
            }
            //First car always creates our first fleet. If above if is not entered afterwards, that means that car 
            //we're looking at did not join this exisiting fleet before arriving at the destination
            //(as it was too slow) and so must then create its own car fleet.
            else
            {
                fleetCount++;
            }
        }
        return fleetCount;
    }

    static int LargestRectangleArea(int[] heights)
    {
        int maxArea = 0;
        //int[] will be in format [index, height]
        Stack<int[]> stack = new Stack<int[]>(); 

        //Iterate over input arr
        for (int i = 0; i < heights.Length; i++)
        {
            int start = i;
            //If stack is not empty and top value's height is greater than the height we're currently evaluating...
            while (stack.Count > 0 && stack.Peek()[1] > heights[i])
            {
                int[] top = stack.Pop();
                int index = top[0];
                int height = top[1];
                //i - index here is width, naturally
                maxArea = Math.Max(maxArea, height * (i - index));
                //Account for current rectangles new start index, is item(s) we just popped were TALLER than it; this means
                //that we need to move the current rectangles starting index back to the index of the LAST rectangle we pop
                //from the stack here
                start = index;
            }
            //Push the new rectangle to the stack with adjusted start index and its height
            stack.Push(new int[] { start, heights[i] });
        }

        //Deal with 'leftovers' in the stack. These leftovers represent rectangles that could have continued 'expanding' right
        //given a larger input. Since this is the case, their ending index is going to be to the end of the input arr, heights.
        //This is represented in the width portion of the formula: heights.Length - index
        foreach (int[] pair in stack)
        {
            int index = pair[0];
            int height = pair[1];
            maxArea = Math.Max(maxArea, height * (heights.Length - index));
        }
        return maxArea;
    }
}