using System.Windows;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("UTest")]
namespace Binary_Tree
{
    public partial class MainWindow : Window
    {
        // Requirement:
        // String will always contain basic math operations + - x ÷ ( ) on whole numbers (both positive and negative)
        // Input string is infix expression
        // Output tree is infix expression

        TreeViewModel viewModel = new TreeViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
