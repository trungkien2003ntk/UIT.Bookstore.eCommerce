using System.Text.RegularExpressions;

namespace KKBookstore.Services;

public static partial class SearchTextFormatter
{
    public static string FormatSearchTextWithAnd(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
            return searchText;

        var matches = QuotedPhraseOrWordMatcher().Matches(searchText);

        var terms = matches.Cast<Match>()
            .Select(m => m.Groups["phrase"].Success ? m.Groups["phrase"].Value.Trim('\"') : m.Groups["word"].Value)
            .SelectMany(term => term.Split(' ', StringSplitOptions.RemoveEmptyEntries)) // split into words
            .Select(word => $"\"{word.Trim()}*\"") // wrap each word with quotes and add wildcard
            .ToList();

        return string.Join(" AND ", terms);
    }

    public static string FormatSearchTextWithOr(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
            return searchText;

        var terms = ExtractTermsFromSearchText(searchText)
            .Select(AppendWildcardToEachWord)
            .ToList();

        return string.Join(" OR ", terms);
    }

    private static string AppendWildcardToEachWord(string term)
    {
        // Remove leading/trailing quotes (if present)
        string cleaned = term.Trim('\"');

        // Add * to each word
        var parts = cleaned.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var wildcarded = parts.Select(word => word + "*");

        return $"\"{string.Join(" ", wildcarded)}\"";
    }

    private static List<string> ExtractTermsFromSearchText(string searchText)
    {
        var matches = QuotedPhraseOrWordMatcher().Matches(searchText);
        var terms = matches.Cast<Match>()
                           .Select(m => m.Groups["phrase"].Success ? m.Groups["phrase"].Value : m.Groups["word"].Value)
                           .Select(m => m.Trim())
                           .Where(m => !string.IsNullOrEmpty(m))
                           .ToList();

        return terms;
    }

    [GeneratedRegex(@"(?<phrase>""[^""]+"")|(?<word>\S+)")]
    private static partial Regex QuotedPhraseOrWordMatcher();
}
