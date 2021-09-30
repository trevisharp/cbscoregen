using System;

public static class SearchExtesion
{
    public static string ScopeSearch(this string txt, string start, string end)
    {
        (int startindex, int endindex) = indexscopesearch(txt, start, end);
        return txt.Substring(startindex, endindex - startindex + 1);
    }

    public static string ScopeSkipSearch(this string txt, string start, string end)
    {
        (int startindex, int endindex) = indexscopesearch(txt, start, end);
        return txt.Substring(endindex + end.Length, txt.Length - endindex - end.Length);
    }

    public static string ScopeSkipSearch(this string txt, string start, string end, int count)
    {
        var result = txt;
        while (count-- > 0)
        {
            (int startindex, int endindex) = indexscopesearch(result, start, end);
            result = result.Substring(endindex + end.Length, result.Length - endindex - end.Length);
        }
        return result;
    }

    private static (int, int) indexscopesearch(string txt, string start, string end)
    {
        int startindex = -1, endindex = -1;
        int level = 0;
        for (int i = 0, j; i < txt.Length; i++)
        {
            j = 0;
            for (; j < start.Length && txt[i + j] == start[j]; j++);
            if (j == start.Length)
            {
                if (startindex == -1)
                    startindex = i;
                else level++;
            }
            j = 0;
            for (; j < end.Length && txt[i + j] == end[j]; j++);
            if (j == end.Length && startindex != -1)
            {
                if (level == 0)
                {
                    endindex = i;
                    break;
                }
                else level--;
            }
        }
        return (startindex, endindex);
    }
}