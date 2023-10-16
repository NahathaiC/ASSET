import * as yup from 'yup';

export const validationSchema = yup.object({
  title: yup.string().required(),
  createDate: yup.string().required(),
  useDate: yup.string().required(),
  department: yup.string().required(),
  section: yup.string().required(),
  prodDesc: yup.string(),
  model: yup.string(),
  quantity: yup.number().required().moreThan(0),
  unitPrice: yup.number().required().moreThan(1),
  prPicture: yup.mixed(),
});
