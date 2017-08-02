using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Workday.Test
{
    [TestClass]
    public class WorkdayViewModelTest
    {
        [TestMethod]
        public void TestWorkdayCommandEnablesWorkingCommand()
        {
            var model = new WorkdayViewModel();

            Assert.IsFalse(model.BlockCommand.CanExecute(null));

            model.WorkdayCommand.Execute(null);

            Assert.IsTrue(model.BlockCommand.CanExecute(null));
        }

        [TestMethod]
        public void TestWorkdayCommandDisablesWorkingCommand()
        {
            var model = new WorkdayViewModel();

            Assert.IsFalse(model.BlockCommand.CanExecute(null));

            model.WorkdayCommand.Execute(null);
            model.WorkdayCommand.Execute(null);

            Assert.IsFalse(model.BlockCommand.CanExecute(null));
        }
    }
}
