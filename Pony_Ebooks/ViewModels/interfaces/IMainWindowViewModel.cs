// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - IMainWindowViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 8:37 PM, 23/01/2015
// //  Created Date: 8:37 PM, 23/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================

using System.Collections.ObjectModel;

using Pony_Ebooks.Framework;

namespace Pony_Ebooks {
    public interface IMainWindowViewModel {

        ICurrentStatusViewModel CurrentStatusViewModel { get; set; }

        ICommandRowViewModel CommandRowViewModel { get; set; }

        ObservableCollection<ITabViewModel> TabViewModels { get; set; } 


    }
}