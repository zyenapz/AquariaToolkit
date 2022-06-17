import 'package:flutter/material.dart';

class PortalPage extends StatefulWidget {
  const PortalPage({Key? key}) : super(key: key);

  @override
  State<PortalPage> createState() => _PortalPageState();
}

class _PortalPageState extends State<PortalPage> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text(
          'A Portal',
          style: TextStyle(
            fontWeight: FontWeight.bold,
            fontSize: 30,
            color: Colors.white,
            fontFamily: 'Baloo Da 2',
          ),
        ),
        centerTitle: true,
      ),
      body: Center(
        child: Column(
          children: [
            const Text('Hello'),
            Image.asset('assets/images/fish.jpg'),
          ],
        ),
      ),
    );
  }
}
