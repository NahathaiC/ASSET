import { Asset } from "../../app/models/asset";
import AssetList from "./AssetList";

interface Props{
    assets: Asset[];
}

export default function AssetCatalog({assets}: Props) {
    return (
        <>
            <AssetList assets={assets}/>
        </>
    )
}
