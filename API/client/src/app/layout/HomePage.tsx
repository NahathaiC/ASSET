import { Box, Typography } from "@mui/material";
import Slider from "react-slick";

export default function HomePage() {
  const settings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,
  };

  return (
    <>
      <Slider {...settings}>
        <div>
          <img
            src="images/LogoHeader.png"
            alt="LogoHeader"
            style={{ display: "block", width: "100%", maxHeight: 500 }}
          />
        </div>
      </Slider>

      <Box display="flex" justifyContent="center" sx={{ p: 4 }}>
        <Typography variant="h1">
          {" "}
          ♥ ยินดีต้อนรับสู่ระบบเว็บแอพพลิเคชัน ระบบจัดการการขอซื้อทรัพย์สิน ♥{" "}
        </Typography>
      </Box>
    </>
  );
}
