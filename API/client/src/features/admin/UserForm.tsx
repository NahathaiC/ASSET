import { useAppDispatch } from "../../app/store/configureStore";
import { UserDto } from "../../app/models/user";
import { useEffect } from "react";
import { LoadingButton } from "@mui/lab";
import { Box, Paper, Typography, Grid, Button } from "@mui/material";
import AppTextInput from "../../app/components/AppTextInput";
import { useForm } from "react-hook-form";
import agent from "../../app/api/agent";
import { setUser } from "../account/accountSlice";
import { yupResolver } from "@hookform/resolvers/yup";
import { validationSchema } from "./userValidation";

interface Props {
  user?: UserDto;
  cancelEdit: () => void;
}
//ตัวแทน productform
export default function UserForm({ user, cancelEdit }: Props) {
  const {
    control,
    reset,
    handleSubmit,
    formState: { isSubmitting },
  } = useForm({
    resolver: yupResolver<any>(validationSchema),
  });

  const dispatch = useAppDispatch();

  useEffect(() => {
    if (user) reset(user);
  }, [user, reset]);

  async function handleSubmitData(data: UserDto) {
    try {
      let response: UserDto | undefined = undefined; // Initialize with undefined
      if (user) {
        response = await agent.User.updateUser(data);
      }
      dispatch(setUser(response));
      cancelEdit();
    } catch (error) {
      console.log(error);
    }
  }

  return (
    <Box component={Paper} sx={{ p: 4 }}>
      <Typography variant="h4" gutterBottom sx={{ mb: 4 }}>
        รายละเอียดบัญชีผู้ใช้
      </Typography>

      <form onSubmit={handleSubmit(handleSubmitData)}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={12}>
            <AppTextInput control={control} name="id" label="รหัสพนักงาน" />
          </Grid>
          <Grid item xs={12} sm={12}>
            <AppTextInput
              control={control}
              name="userName"
              label="ชื่อบัญชีผู้ใช้"
            />
          </Grid>
          <Grid item xs={12} sm={12}>
            <AppTextInput control={control} name="email" label="อีเมลล์" />
          </Grid>
          <Grid item xs={12} sm={12}>
            <AppTextInput control={control} name="position" label="ตำแหน่ง" />
          </Grid>
          <Grid item xs={12} sm={12}>
            <AppTextInput control={control} name="department" label="สาขา" />
          </Grid>
          <Grid item xs={12} sm={12}>
            <AppTextInput control={control} name="section" label="แผนก" />
          </Grid>
          <Grid item xs={12} sm={12}>
            <AppTextInput control={control} name="phone" label="เบอร์โทร" />
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
