// // ==========================================================================================================
// // 
// //  File ID: Pony_Ebooks - Pony_Ebooks - ICurrentStatusViewModel.cs 
// // 
// //  Last Changed By: Collin O'Connor - Ridayah
// //  Last Changed Date: 8:38 PM, 23/01/2015
// //  Created Date: 8:38 PM, 23/01/2015
// // 
// //  Notes:
// //  
// // ==========================================================================================================
namespace Pony_Ebooks {
    public interface ICurrentStatusViewModel {

        string NextChain { get; set; }

        string PostTime { get; set; }

        string CountdownTimer { get; set; }

    }
}