import * as yup from 'yup';

export const validationSchema = yup.object({
  title: yup.string().required('จำเป็นต้องใส่'),
  createDate: yup.string().required('จำเป็นต้องใส่'),
  useDate: yup.string().required('จำเป็นต้องใส่'),
  department: yup.string().required('จำเป็นต้องใส่'),
  section: yup.string().required('จำเป็นต้องใส่'),
  prodDesc: yup.string().required('จำเป็นต้องใส่'),
  model: yup.string(),
  quantity: yup.number().required('จำนวนห้ามติดลบ').moreThan(0),
  unitPrice: yup.number().required('จำนวนห้ามติดลบ').moreThan(0),
  prPicture: yup.mixed(),
});
