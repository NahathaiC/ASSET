import { Box, Button, Grid, Paper, Typography } from "@mui/material";
import { useForm, FieldValues } from "react-hook-form";
import { PurchaseRequisition } from "../../app/models/purchaseRequisition";
import AppTextInput from "../../app/components/AppTextInput";
import AppDropzone from "../../app/components/AppDropzone";
import { LoadingButton } from "@mui/lab";
import { useEffect } from "react";
import agent, { fetchCurrentUser } from "../../app/api/agent";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { setUser } from "../account/accountSlice";
import { yupResolver } from '@hookform/resolvers/yup';
import { validationSchema } from "./prValidation";

interface Props {
  purchaserequisition?: PurchaseRequisition;
  cancelEdit: () => void;
}

export default function PRForm({ purchaserequisition, cancelEdit }: Props) {
  const { control, handleSubmit, watch, formState: { isSubmitting } } = useForm({
    resolver: yupResolver<any>(validationSchema)
});
  const watchFile = watch("prPicture", null);
  const currentUser = useAppSelector((state) => state.account.user);
  const dispatch = useAppDispatch();

const fetchUser = async () => {
   try {
     if (!currentUser) {
       const user = await fetchCurrentUser(); // Call fetchCurrentUser directly here
       dispatch(setUser(user));
     }
   } catch (error) {
     console.error(error);
   }
 };

 useEffect(() => {
   fetchUser(); // Call the async function to fetch user data
 }, [currentUser, dispatch]);

  async function handleSubmitData(data: FieldValues) {
    try {
        let response: PurchaseRequisition;
        if (purchaserequisition) {
            response = await agent.Admin.createPR(data);
            console.log(data);
        } else {
            response = await agent.Admin.createPR(data);
            console.log(data);
        };
    } catch (error) {
        console.log(error);
    }
}

  return (
    <Box component={Paper} sx={{ p: 4 }}>
      <Typography variant="h4" gutterBottom sx={{ mb: 4 }}>
        ฟอร์มการขอซื้อ
      </Typography>


      <form onSubmit={handleSubmit(handleSubmitData)}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={12}>
            <AppTextInput control={control} name="title" label="ชื่อหัวข้อ" />
          </Grid>
          <Grid item xs={12} sm={6}>
            <Typography variant="subtitle1" gutterBottom>
              วันที่ขอซื้อ
            </Typography>
            <AppTextInput
              type="date"
              control={control}
              name="createDate"
              label={""}
            />
          </Grid>

          <Grid item xs={12} sm={6}>
            <Typography variant="subtitle1" gutterBottom>
              วันที่ต้องการใช้
            </Typography>
            <AppTextInput
              type="date"
              control={control}
              name="useDate"
              label={""}
            />
          </Grid>

          <Grid item xs={12} sm={6}>
            <AppTextInput control={control} name="department" label="สาขา" />
          </Grid>
          <Grid item xs={12} sm={6}>
            <AppTextInput control={control} name="section" label="แผนก" />
          </Grid>
          <Grid item xs={12}>
            <AppTextInput
              multiline={true}
              rows={4}
              control={control}
              name="prodDesc"
              label="รายละเอียดสินค้า"
            />
          </Grid>
          <Grid item xs={12}>
            <AppTextInput control={control} name="model" label="รุ่น/โมเดล" />
          </Grid>
          <Grid item xs={12}>
            <AppTextInput
              type="number"
              control={control}
              name="quantity"
              label="จำนวน"
            />
          </Grid>
          <Grid item xs={12}>
            <AppTextInput
              type="number"
              control={control}
              name="unitPrice"
              label="ราคาต่อหน่วย"
            />
          </Grid>
          {/* <Grid item xs={12}>
            <AppTextInput control={control} name="remark" label="หมายเหตุ" />
          </Grid> */}
          <Grid item xs={12}>
            <Box
              display="flex"
              justifyContent="space-between"
              alignItems="center"
            >
              <AppDropzone control={control} name="prPicture" />
              {watchFile ? (
                <img
                  src={watchFile.preview}
                  alt="preview"
                  style={{ maxHeight: 200 }}
                />
              ) : (
                <img
                  src={purchaserequisition?.prPicture}
                  alt={purchaserequisition?.title}
                  style={{ maxHeight: 200 }}
                />
              )}
            </Box>
          </Grid>
        </Grid>
        <Box display="flex" justifyContent="space-between" sx={{ mt: 3 }}>
          <Button onClick={cancelEdit} variant="contained" color="inherit">
            Cancel
          </Button>
          <LoadingButton
            loading={isSubmitting}
            type="submit"
            variant="contained"
            color="success"
          >
            Submit
          </LoadingButton>
        </Box>
      </form>
    </Box>
  );
}
