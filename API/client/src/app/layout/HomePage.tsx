import { Box, Typography } from "@mui/material";
import Slider from "react-slick";

export default function HomePage() {
  const settings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,
    autoplay: true,          // Enable automatic rotation
    autoplaySpeed: 3000,    // Set the rotation speed in milliseconds (e.g., 3 seconds)
  };

  return (
    <>
      <Slider {...settings}>
        <div>
          <img
            src="/LogoHeader.png"
            alt="LogoHeader"
            style={{ display: "block", width: "100%", maxHeight: 500 }}
          />
        </div>
        <div>
          <img
            src="/LogoHeader2.png"
            alt="LogoHeader2"
            style={{ display: "block", width: "100%", maxHeight: 500 }}
          />
        </div>
        <div>
          <img
            src="/LogoHeader3.png"
            alt="LogoHeader3"
            style={{ display: "block", width: "100%", maxHeight: 500 }}
          />
        </div>
      </Slider>

      <Box display="flex" justifyContent="center" sx={{ p: 4 }}>
        <Typography align="center">
          {" "}
          ยินดีต้อนรับสู่เว็บแอพพลิเคชัน ระบบจัดการการขอซื้อทรัพย์สิน{" "}
          <br />
          {" "}
          เข้าสู่ระบบ และสร้างใบขอซื้อของคุณได้ทันที ! {" "}
        </Typography>
      </Box>
    </>
  );
}
