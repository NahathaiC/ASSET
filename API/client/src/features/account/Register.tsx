import Avatar from "@mui/material/Avatar";
import TextField from "@mui/material/TextField";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { Paper } from "@mui/material";
import { Link, useNavigate } from "react-router-dom";
import { LoadingButton } from "@mui/lab";
import { useForm } from "react-hook-form";
import agent from "../../app/api/agent";
import { toast } from "react-toastify";

export default function Register() {
  const navigate = useNavigate();
  const { register, handleSubmit, setError, formState: { isSubmitting, errors, isValid }, 
        } = useForm({ mode: "onTouched", });

  function handleApiErrors(errors: any) {
    if (errors) 
    {
      errors.forEach((error: string) => {
        if (error.includes("Password")) {
          setError("password", { message: error });
        } else if (error.includes("Email")) {
          setError("email", { message: error });
        } else if (error.includes("Username")) {
          setError("name", { message: error });
        } else if (error.includes("Id")) {
          setError("id", { message: error });
        }
      });
    }
  }

  return (
    <Container
      component={Paper}
      maxWidth="sm"
      sx={{
        p: 4,
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
      }}
    >
      <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
        <LockOutlinedIcon />
      </Avatar>
      <Typography component="h1" variant="h5">
        สร้างบัญชีผู้ใช้
      </Typography>
      <Box
        component="form"
        onSubmit={handleSubmit((data) =>
          agent.Account.register(data)
            .then(() => {
              toast.success("สร้างบัญชีสำเร็จ");
              navigate("/login");
            })
            .catch((error) => handleApiErrors(error))
        )}
        noValidate
        sx={{ mt: 1 }}
      >
        <TextField
          margin="normal"
          fullWidth
          label="ชื่อ-สกุล"
          autoFocus
          {...register("name", { required: true })}
          error={!!errors.name}
          helperText={errors?.name?.message as string}
        />
        <TextField
          margin="normal"
          fullWidth
          label="รหัสพนักงาน"
          {...register("id", { required: true })}
          error={!!errors.id}
          helperText={errors?.id?.message as string}
        />
        <TextField
          margin="normal"
          fullWidth
          label="อีเมลล์"
          {...register("email", {
            required: true,
            pattern: {
              value: /^([a-zA-Z0-9_\-.]+)@([a-zA-Z0-9_\-.]+)\.([a-zA-Z]{2,5})$/,
              message: "อีเมลล์ไม่ถูกต้อง",
            },
          })}
          error={!!errors.email}
          helperText={errors?.email?.message as string}
        />
        <TextField
          margin="normal"
          fullWidth
          label="รหัสผ่าน"
          type="password"
          {...register("password", {
            required: true,
            pattern: {
              value: /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$/,
              message: "รหัสผ่านต้องประกอบด้วย อักขระพิเศษ และตัวเลข",
            },
          })}
          error={!!errors.password}
          helperText={errors?.password?.message as string}
        />
        <TextField
          margin="normal"
          fullWidth
          label="ตำแหน่ง"
          {...register("position", { required: true })}
          error={!!errors.position}
          helperText={errors?.position?.message as string}
        />

        <TextField
          margin="normal"
          fullWidth
          label="สาขา"
          {...register("department", { required: true })}
          error={!!errors.department}
          helperText={errors?.department?.message as string}
        />

        <TextField
          margin="normal"
          fullWidth
          label="แผนก"
          {...register("section", { required: true })}
          error={!!errors.section}
          helperText={errors?.section?.message as string}
        />

        <TextField
          margin="normal"
          fullWidth
          label="เบอร์โทรศัพท์มือถือ"
          {...register("phone", { required: true })}
          error={!!errors.phone}
          helperText={errors?.phone?.message as string}
        />
        <LoadingButton
          loading={isSubmitting}
          disabled={!isValid}
          type="submit"
          fullWidth
          variant="contained"
          sx={{ mt: 3, mb: 2 }}
        >
          Register
        </LoadingButton>
        <Grid container>
          <Grid item>
            <Link to="/login">{"Already have an account? Sign In"}</Link>
          </Grid>
        </Grid>
      </Box>
    </Container>
  );
}
