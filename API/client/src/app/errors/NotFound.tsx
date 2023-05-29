import { Button, Container, Divider, Paper, Typography } from "@mui/material";
import { Link } from "react-router-dom";

export default function NotFound() {
    return (
        <Container component={Paper} sx={{height: 400}}>
            <Typography gutterBottom variant="h3">
                Oops - ไม่พบสิ่งที่คุณกำลังตามหา
            </Typography>
            <Divider />
            <Button fullWidth component={Link} to='/'>กลับไปหน้าแรก</Button>
        </Container>
    )
}