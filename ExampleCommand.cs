using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using Microsoft.VisualStudio.Shell;

namespace VSIXContextMenus
{
    internal sealed class ExampleCommand
    {
        public const int CommandId = 0x0100;
        public static readonly Guid CommandSet = new Guid("b827c1d5-b9e8-4370-9451-a60a3e14d7ce");
        readonly Package package;

        ExampleCommand(Package package)
        {
            if (package == null)
                throw new ArgumentNullException(nameof(package));

            this.package = package;

            var commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandId = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandId);
                commandService.AddCommand(menuItem);
            }
        }

        public static void Initialize(Package package)
        {
            Instance = new ExampleCommand(package);
        }

        void MenuItemCallback(object sender, EventArgs e)
        {
            Debug.WriteLine("Hello World");
        }

        public static ExampleCommand Instance { get; private set; }
        IServiceProvider ServiceProvider => this.package;
    }
}
