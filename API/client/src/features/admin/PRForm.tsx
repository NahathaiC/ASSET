import { Box, Button, FormControl, Grid, InputLabel, MenuItem, Paper, Select, Typography } from "@mui/material";
import { useForm, Controller, SubmitHandler } from "react-hook-form"; // Import SubmitHandler
import { PurchaseRequisition } from "../../app/models/purchaseRequisition";

interface FormValues { // Define a type for form values
  approvalStatus: string;
}

interface Props {
  purchaserequisition?: PurchaseRequisition;
  cancelEdit: () => void;
}

export default function PRForm({ purchaserequisition, cancelEdit }: Props) {
  const { control, handleSubmit } = useForm<FormValues>(); // Specify FormValues as the generic type

  const onSubmit: SubmitHandler<FormValues> = (data) => { // Add SubmitHandler with FormValues type
    // Handle the form submission, including the "Approval Status" choice.
    console.log(data);
  };

  return (
    <Box component={Paper} sx={{ p: 4 }}>
      <Typography variant="h4" gutterBottom sx={{ mb: 4 }}>
        Purchase Requisition Details
      </Typography>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Grid container spacing={3}>
          <Grid item xs={12}>
            <Controller
              name="approvalStatus"
              control={control}
              defaultValue=""
              render={({ field }) => (
                <FormControl>
                  <InputLabel>Approval Status</InputLabel>
                  <Select {...field}>
                    <MenuItem value="">Select</MenuItem>
                    <MenuItem value="Approved">Approved</MenuItem>
                    <MenuItem value="Disapproved">Disapproved</MenuItem>
                  </Select>
                </FormControl>
              )}
            />
          </Grid>
        </Grid>
        <Box display="flex" justifyContent="space-between" sx={{ mt: 3 }}>
          <Button onClick={cancelEdit} variant="contained" color="inherit">
            Cancel
          </Button>
          <Button type="submit" variant="contained" color="success">
            Submit
          </Button>
        </Box>
      </form>
    </Box>
  );
}
