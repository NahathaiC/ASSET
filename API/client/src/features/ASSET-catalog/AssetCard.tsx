import {
  Avatar,
  Card,
  CardHeader,
  CardMedia,
  Typography,
  CardContent,
  CardActions,
  Button,
} from "@mui/material";
import { Asset } from "../../app/models/asset";
import { Link } from "react-router-dom";

interface Props {
  asset: Asset;
}

export default function AssetCard({ asset }: Props) {
  const getStatusColor = (assetStatus: string): string => {
    if (assetStatus.charAt(0) === "G") {
      return "#4BCEFF";
    } else if (assetStatus.charAt(0) === "N") {
      return "#66bb6a";
    } else if (assetStatus.charAt(0) === "U") {
      return "#ffb74d";
    } else if (assetStatus.charAt(0) === "B") {
      return "#d32f2f";
    } else {
      return "inherit";
    }
  };

  const statusColor = getStatusColor(asset.assetStatus);
  return (
    <Card>
      <CardHeader
        avatar={
          <Avatar style={{ backgroundColor: statusColor }}>
            {asset.name.charAt(0)}
          </Avatar>
        }
        title={asset.name}
        titleTypographyProps={{
          sx: { fontWeight: "bold" },
        }}
      />
      <CardMedia
        sx={{ height: 140, backgroundSize: "contain", bgcolor: "grey.300" }}
        image={asset.assetPic}
      />
      <CardContent>
      <br />
        <Typography sx={{ fontSize: 14 }} color="text.primary" gutterBottom>
          {"ใช้งานโดย : "}
          {asset.personInCharge.userName}
        </Typography>
        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
          {"ผู้ผลิต : "} {asset.manufacturer}
          <br /> {"โมเดล : "} {asset.model}
          <br /> {"ประเภททรัพย์สิน : "} {asset.type}
        </Typography>
        <br />
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
          {"สถานะ : "}{asset.assetStatus}
        </Typography>
      </CardContent>
      <CardActions>
        <Button
          component={Link}
          to={`/asset-catalog/${encodeURIComponent(asset.id)}`}
          size="small"
        >
          {" "}
          View{" "}
        </Button>
      </CardActions>
    </Card>
  );
}
