import { Box, Button, List, ListItem, Typography } from "@mui/material";
import { useState } from "react";
import PRForm from "../admin/PRForm";
import { NavLink } from "react-router-dom";
import { useAppSelector } from "../../app/store/configureStore";

export default function PurchaseMN() {
  const [editMode, setEditMode] = useState(false);

  // Define a function to handle canceling the edit mode
  const cancelEdit = () => {
    setEditMode(false);
  };
  const { user } = useAppSelector((state) => state.account);

  if (editMode) return <PRForm cancelEdit={cancelEdit} />;

  return (
    <>
      <Box display="flex" justifyContent="space-between">
        <List sx={{ display: "flex", flexDirection: "column" }}>
          {user && user.roles?.includes("Approver") && (
            <ListItem>
              <Button
                component={NavLink}
                to={"/prstatus"}
                variant="contained"
                size="large"
                sx={{
                  width: "100%", // Make the button equally wide
                }}
              >
                จัดการสถานะการขอจัดซื้อ
              </Button>
            </ListItem>
          )}

          {user && user.roles?.includes("Approver") && (
            <ListItem>
              <Button
                component={NavLink}
                to={"/postatus"}
                variant="contained"
                size="large"
                sx={{
                  width: "100%", // Make the button equally wide
                }}
              >
                จัดการสถานะการจัดซื้อ
              </Button>
            </ListItem>
          )}
        </List>
        {/* Add other content or buttons here */}
      </Box>
    </>
  );
}
