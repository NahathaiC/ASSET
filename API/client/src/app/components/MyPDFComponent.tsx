import React from 'react';
import { Document, Page, View, Text, StyleSheet } from '@react-pdf/renderer';
import { PurchaseRequisition } from '../models/purchaseRequisition';

interface MyPDFProps {
  formData: PurchaseRequisition; // Define your data structure here
}

const MyPDFComponent: React.FC<MyPDFProps> = ({ formData }) => {
  return (
    <Document>
      <Page size="A4">
        <View style={styles.container}>
          <Text style={styles.heading}>เอกสารใบขอซื้อทรัพย์สิน</Text>
          <Text style={styles.text}>{formData.title}</Text>
          <Text style={styles.text}>{formData.requestUser}</Text>
          <Text style={styles.text}>{formData.createDate}</Text>
          <Text style={styles.text}>{formData.useDate}</Text>
          <Text style={styles.text}>{formData.department}</Text>
          <Text style={styles.text}>{formData.section}</Text>
          <Text style={styles.text}>{formData.prodDesc}</Text>
          <Text style={styles.text}>{formData.model}</Text>
          <Text style={styles.text}>{formData.quantity}</Text>
          <Text style={styles.text}>{formData.unitPrice}</Text>
          <Text style={styles.text}>{formData.prPicture}</Text>
        </View>
      </Page>
    </Document>
  );
};

const styles = StyleSheet.create({
  container: {
    margin: 10,
  },
  heading: {
    fontSize: 20,
    marginBottom: 10,
  },
  text: {
    fontSize: 14,
    marginBottom: 5,
  },
});

export default MyPDFComponent;
