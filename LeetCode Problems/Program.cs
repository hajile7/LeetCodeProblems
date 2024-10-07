//Problems:
//Group Anagrams
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
static int[] testFunc(int[] nums)
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