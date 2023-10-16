import { Box, Button, Typography } from "@mui/material"
import { useState } from "react"
import PRForm from "../admin/PRForm";

export default function HomePage() {
    const [editMode, setEditMode] = useState(false);

    if (editMode) return <PRForm cancelEdit={function (): void {
        throw new Error("Function not implemented.");
    } }/>

    return (
        <>
            <Box display='flex' justifyContent='space-between'>
                <Typography sx={{p:2}} variant="h4" > กรอกฟอร์มขอซื้อทรัพย์สินได้ที่นี่ </Typography>
                <Button onClick={() => setEditMode(true)} sx={{ m:2 }} size="large" variant="contained"> สร้าง </Button>
            </Box>
        </>
    )
}