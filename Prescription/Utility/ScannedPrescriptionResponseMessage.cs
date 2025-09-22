using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prescription.Utility
{
    public static class ScannedPrescriptionResponseMessage
    {
        public const string common_null_of_get_list = "ERROR: There is no data available.";
        public const string common_get_all_success = "SUCCESS: gets successfully.";
        public const string common_see_try_catch = "ERROR: Something went wrong. Please check the try-catch block.";
        public const string common_insert_success_message = "Data inserted successfully !";
        public const string common_inserted_failed_message = "Data is not inserted successfully !";
        public const string common_deleted_failed_message = "Data is not deleted !";
        public const string common_delete_success_message = "Data is deleted successfully !";
        public const string common_update_success_message = "Data is updated successfully !";
        public const string common_update_failed_message = "Data is not updated !";
        public const string common_get_by_id_success = "Data is get successfully !";
    }
}
