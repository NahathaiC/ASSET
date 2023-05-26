import { Avatar, CardActions, CardHeader, CardMedia } from "@mui/material";
import { PurchaseRequisition } from "../../app/models/purchaseRequisition";
import Box from "@mui/material/Box";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
import { Link } from "react-router-dom";

interface Props {
  purchaserequisition: PurchaseRequisition;
}

export default function PRCard({ purchaserequisition }: Props) {
  const bull = (
    <Box
      component="span"
      sx={{ display: "inline-block", mx: "2px", transform: "scale(0.8)" }}
    >
      •
    </Box>
  );

  const getStatusColor = (status: string): string => {
    if (status.charAt(0) === "P") {
      return "#ffb74d";
    } else if (status.charAt(0) === "D" || status.charAt(0) === "C") {
      return "#d32f2f";
    } else if (status.charAt(0) === "A") {
      return "#66bb6a";
    } else {
      return "inherit";
    }
  };

  const statusColor = getStatusColor(purchaserequisition.status);
  const formattedDate = purchaserequisition.createDate.slice(0, 10);
  const formatteduseDate = purchaserequisition.useDate.slice(0, 10);

  return (
    <Card>
      <CardHeader
        avatar={
          <Avatar style={{ backgroundColor: statusColor }}>
            {purchaserequisition.status.charAt(0).toUpperCase()}
          </Avatar>
        }
        title={purchaserequisition.title}
        titleTypographyProps={{
          sx: { fontWeight: "bold" },
        }}
      />
      <CardMedia
        sx={{ height: 140, backgroundSize: 'contain', bgcolor: 'grey.300' }}
        image={purchaserequisition.prPicture}
      />
      <CardContent>
        <Typography sx={{ fontSize: 14 }} color="text.primary" gutterBottom>
          {"สร้างโดย : "}
          {purchaserequisition.requestUser}
        </Typography>
        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
          {"วันที่ขอซื้อ : "} {formattedDate}
          <br /> {"วันที่ต้องการใช้ : "} {formatteduseDate}
          <br /> {"สาขา : "} {purchaserequisition.department}
          <br /> {"แผนก : "} {purchaserequisition.section}
        </Typography>
        <Typography
          sx={{
            fontSize: 14,
            backgroundColor: statusColor,
            borderRadius: "8px",
            padding: "4px 8px",
            color: "#fff",
            display: "inline-block",
          }}
          gutterBottom
        >
          {purchaserequisition.status}
        </Typography>
      </CardContent>
      <CardActions>
        <Button component={Link} to={`/catalog/${purchaserequisition.id}`} size="small"> View </Button>
      </CardActions>
    </Card>
  );
}
