# Cash Desk device mocks for the CoCoME system

This repository contains a WPF application to mock typical cash desk devices such as a card reader, a display and a barcode reader. The functionality of these devices is exposed as SiLA2 services.

# Example Client

The solution also consists of an example client. For this, both the terminal application and the bank server need to be started. If this is the case, the code in `Program.cs` demonstrates how to use the generated clients to read barcodes, display things on the display or complete a transaction.

# Terminal

The terminal application consists of three areas, each representing a single device: A barcode reader, a cardreader and a display controller. Each device is controlled using a separate SiLA2 feature.

To scan an item, you can either press one of the predefined product icons or enter the barcode manually. If there are more products you want to use (for demo purposes), feel free to add more buttons. You can use the existing command for reading the barcode, you only need to define the actual barcode as command parameter. 

The card reader mock works similarly but has its own, separate display (like most card readers do). The buttons in that area can be used to mock that the transaction was completed with different cards. Feel free to add more cards, again by adding buttons with the existing command and the account number as command parameter.