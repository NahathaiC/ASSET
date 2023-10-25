import { ListItem, ListItemAvatar, Avatar, ListItemText } from "@mui/material";
import { Asset } from "../../app/models/asset";

interface Props {
    asset: Asset;
  }

export default function AssetCard({asset}:Props) {
  return (
    <ListItem key={asset.id}>
      <ListItemAvatar>
        <Avatar src={asset.assetPic} />
      </ListItemAvatar>

      <ListItemText>{asset.name}</ListItemText>
    </ListItem>
  );
}
