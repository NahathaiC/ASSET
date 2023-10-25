import { List } from "@mui/material";
import { Asset } from "../../app/models/asset";
import AssetCard from "./AssetCard";

interface Props {
  assets: Asset[];
}

export default function AssetList({ assets }: Props) {
  return (
    <List>
      {assets.map((asset) => (
        <AssetCard key={asset.id} asset={asset} />
      ))}
    </List>
  );
}
