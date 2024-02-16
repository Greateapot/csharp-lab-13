using ConsoleIOLib;
using ConsoleDialogLib;

using Lab10Lib.Entities;

using Lab12Lib.Exceptions;
using Lab10Lib.Utils;

namespace Lab13
{
    public class Program
    {
        private static int InputCapacity() => ConsoleIO.Input<int>(
            Messages.InputCapacity,
            v => v < 1
                ? Messages.InputCapacityValueLessThanOne
                : null
        );

        private static int InputCount(int capacity = 0) => ConsoleIO.Input<int>(
            Messages.InputCount,
            v => v < 1
                ? Messages.InputCountValueLessThanOne
                : capacity > 0 && v > capacity
                    ? Messages.InputCountValueLessThanCapacity
                    : null
        );

        private static bool InputBoolean(string title) => (bool)ConsoleDialog.ShowDialog(new(
            title,
            [
                new ConsoleDialogOption(Messages.BooleanDialogYes, _ => true, true, false, true),
                new ConsoleDialogOption(Messages.BooleanDialogNo, _ => false, true, false, true),
            ],
            false,
            true,
            false
        ))!;

        private static void Main()
        {

            PersonTree? tree = null;
            Journal ja = new("Journal A");
            Journal jb = new("Journal B");
            var exit = false;

            ConsoleDialog dialog = new(
                Messages.Task4DialogTitle,
                [
                    new(Messages.Task4DialogOptionInitUnlimited, _ => Task4ProcessInitUnlimited(ref tree, ref ja, ref jb), true, true, true),
                    new(Messages.Task4DialogOptionInitLimited, _ => Task4ProcessInitLimited(ref tree, ref ja, ref jb), true, true, true),
                    new(Messages.Task4DialogOptionMakeReadOnly, _ => Task4ProcessMakeReadOnly(ref tree), true, true, true),
                    new(Messages.Task4DialogOptionAdd, _ => Task4ProcessAdd(ref tree), true, true, true),
                    new(Messages.Task4DialogOptionRemove, _ => Task4ProcessRemove(ref tree), true, true, true),
                    new(Messages.Task4DialogOptionClear, _ => Task4ProcessClear(ref tree), true, true, true),
                    new(Messages.Task4DialogOptionContains, _ => Task4ProcessContains(ref tree), true, true, true),
                    new(Messages.Task4DialogOptionCompareWithCopies, _ => Task4ProcessCompareWithCopies(ref tree), true, true, true),
                    new(Messages.Task4DialogOptionDispose, _ =>Task4ProcessDispose(ref tree), true, true, true),
                    new(Messages.Task4DialogOptionExit, _ => exit = true, true, true, true),
                ],
                false
            );

            do
            {
                dialog.SubTitle = string.Format(
                    Messages.DialogSubTitle,
                    tree,
                    ja.GlobalKey,
                    string.Join('\n', ja.Select(v => v.ToString())),
                    jb.GlobalKey,
                    string.Join('\n', jb.Select(v => v.ToString()))
                );
                ConsoleDialog.ShowDialog(dialog);
                dialog.Reset();
            } while (!exit);

            tree?.Dispose();
        }

        private static void Task4ProcessInitUnlimited(ref PersonTree? tree, ref Journal ja, ref Journal jb)
        {
            if (tree != null)
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeAlreadyExists);
            else
            {
                tree = []; // net8.0 угарает
                tree.CountChanged += ja.CountChanged;
                tree.CountChanged += jb.CountChanged;
                tree.TreeDisposed += jb.TreeDisposed;
            }
        }

        private static void Task4ProcessInitLimited(ref PersonTree? tree, ref Journal ja, ref Journal jb)
        {
            if (tree != null)
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeAlreadyExists);
            else
            {
                tree = new(InputCapacity());
                tree.CountChanged += ja.CountChanged;
                tree.CountChanged += jb.CountChanged;
                tree.TreeDisposed += jb.TreeDisposed;
            }
        }

        private static void Task4ProcessMakeReadOnly(ref PersonTree? tree)
        {
            if (tree == null)
            {
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeNotExists);
                return;
            }
            tree.IsReadOnly = InputBoolean(Messages.Task4ProcessMakeTreeReadOnly);
            if (tree.IsReadOnly)
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeIsReadOnly);
            else
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeIsNotReadOnly);
        }

        private static void Task4ProcessAdd(ref PersonTree? tree)
        {
            if (tree == null)
            {
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeNotExists);
                return;
            }
            try
            {
                tree.AddAll(PersonGenerator.GetRandomPerson(InputCount(tree.Capacity)));
                ConsoleIO.WriteLine(Messages.Task4ProcessItemAdded);
            }
            catch (CollectionIsReadOnlyException)
            {
                ConsoleIO.WriteLine(Messages.CollectionIsReadOnlyException);
            }
            catch (CollectionIsFullException)
            {
                ConsoleIO.WriteLine(Messages.CollectionIsFullException);
            }
        }

        private static void Task4ProcessRemove(ref PersonTree? tree)
        {
            if (tree == null)
            {
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeNotExists);
                return;
            }
            try
            {
                tree.AddAll(PersonGenerator.GetPerson(InputCount(tree.Capacity)));
                ConsoleIO.WriteLine(Messages.Task4ProcessItemRemoved);
            }
            catch (CollectionIsReadOnlyException)
            {
                ConsoleIO.WriteLine(Messages.CollectionIsReadOnlyException);
            }
        }

        private static void Task4ProcessClear(ref PersonTree? tree)
        {
            if (tree == null)
            {
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeNotExists);
                return;
            }
            try
            {
                tree.Clear();
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeCleared);
            }
            catch (CollectionIsReadOnlyException)
            {
                ConsoleIO.WriteLine(Messages.CollectionIsReadOnlyException);
            }
        }

        private static void Task4ProcessContains(ref PersonTree? tree)
        {
            if (tree == null)
            {
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeNotExists);
                return;
            }
            var person = Person.Init();
            if (tree.Contains(person))
                ConsoleIO.WriteLine(Messages.PersonFound);
            else
                ConsoleIO.WriteLine(Messages.PersonNotFound);
        }

        private static void Task4ProcessCompareWithCopies(ref PersonTree? tree)
        {
            if (tree == null)
            {
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeNotExists);
                return;
            }
            var clone = (PersonTree)tree.Clone();
            var copy = tree.ShallowCopy();
            ConsoleIO.WriteLineFormat(Messages.Task4ProcessIsTreeCloneEquals, clone.Equals(tree));
            ConsoleIO.WriteLineFormat(Messages.Task4ProcessIsTreeCopyEquals, copy.Equals(tree));
        }

        private static void Task4ProcessDispose(ref PersonTree? tree)
        {
            if (tree == null)
            {
                ConsoleIO.WriteLine(Messages.Task4ProcessTreeNotExists);
                return;
            }
            tree.Dispose();
            tree = null;
            ConsoleIO.WriteLine(Messages.Task4ProcessTreeDeleted);
        }

    }
}