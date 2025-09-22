using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannedPrescription.Utility
{
    public static class ScannedPrescriptionApiConstantsResponseMessage
    {
        public const string scanned_prescription_null_of_get_list = "ERROR: There is no data available.";
        public const string scanned_prescription_get_all_success = "SUCCESS: gets successfully.";
        public const string scanned_prescription_see_try_catch = "ERROR: Something went wrong. Please check the try-catch block.";
        public const string scanned_prescription_insert_success_message = "Data inserted successfully !";
        public const string scanned_prescription_inserted_failed_message = "Data is not inserted successfully !";
        public const string scanned_prescription_deleted_failed_message = "Data is not deleted !";
        public const string scanned_prescription_delete_success_message = "Data is deleted successfully !";
        public const string scanned_prescription_update_success_message = "Data is updated successfully !";
        public const string scanned_prescription_update_failed_message = "Data is not updated !";
    }
}
