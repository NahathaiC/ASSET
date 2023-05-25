import { useEffect, useState } from "react";

function App() {
  const [assets, setAssets] = useState([
    { id: "AZ/LPA/100963/001", name: "เครื่องสแกนใบหน้า", type: "เครื่องใช้สำนักงาน" },
    { id: "AZ/LPA/180963/002", name: "อุปกรณ์เชื่อมต่อสัญญาณ Access Point", type: "เครื่องใช้สำนักงาน" },
  ]);

  useEffect(() => {
    fetch ('http://localhost:5050/api/Asset')
      .then(response => response.json())
      .then(data => setAssets(data))
  }, [])

  function addAsset() {
    setAssets([...assets, {id: 'AZ/LPA/180963/003', name: 'อุปกรณ์เชื่อมต่อสัญญาณ Access Point', type: 'เครื่องใช้สำนักงาน'}])
  }

  return (
    <div>
      <h1>ASSET STORE</h1>
      <ul>
        {assets.map((item) => (
          <li key={item.id}>
            {item.id} - {item.name}{" "}
          </li>
        ))}
      </ul>
      <button onClick={addAsset}> Add Asset </button>
    </div>
  );
}

export default App;
