

namespace Lab13Tests;

public class JournalTests
{
    [Fact]
    public void Test()
    {
        // arrange
        PersonTree tree = new("testtree");
        Journal ja = new("ja");
        Journal jb = new("jb");

        tree.CountChanged += ja.CountChanged;
        tree.CountChanged += jb.CountChanged;
        tree.TreeDisposed += jb.TreeDisposed;

        // act
        tree.AddAll(PersonGenerator.GetRandomPerson(4));

        var t1a = ja.Count == 4;
        var t1b = jb.Count == 4;

        {
            var enumerator = tree.GetEnumerator();
            enumerator.MoveNext();
            tree.RemoveAll(enumerator.Current);
            enumerator.Dispose();
        }

        var t2a = ja.Count == 5;
        var t2b = jb.Count == 5;

        tree.Dispose();

        var t3a = ja.Count == 5;
        var t3b = jb.Count == 6;

        // assert
        Assert.True(t1a);
        Assert.True(t1b);

        Assert.True(t2a);
        Assert.True(t2b);

        Assert.True(t3a);
        Assert.True(t3b);
    }
}