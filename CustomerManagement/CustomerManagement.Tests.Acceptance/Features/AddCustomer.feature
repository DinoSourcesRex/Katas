﻿@dbdependent
Feature: AddCustomer
	In order to manage prospective and existing customer records
	As an administrator of the system
	I must be able to add a new user

Background:
	Given the following records in the database
		| Name    | PreviouslyOrdered | WebCustomer | LastActive | FavouriteColours   |
		| Odin    | False             | False       | 02-02-2016 | Red,Blue,Green     |
		| Thor    | False             | True        | 03-02-2016 | Blue,Gold          |
		| Loki    | False             | False       | 01-01-2016 | Black              |
		| Freya   | True              | True        | 12-30-2015 | Yellow             |
		| Bor     | False             | False       | 04-03-2016 | Brown              |
		| Heimdal | True              | True        | 03-22-2016 | Gold,Amber         |
		| Hel     | True              | False       | 04-04-2016 | Red,Brown,Black    |
		| Sif     | True              | True        | 05-24-2015 | Purple,Green       |
		| Thrud   | False             | True        | 04-23-2016 | Pink,Yellow        |
		| Mani    | True              | True        | 01-05-2016 | Red,Pink           |
		| Frigg   | True              | True        | 06-06-2015 | Black,Brown        |
		| Eir     | False             | False       | 08-27-2016 | Blue,Black         |
		| Hoenir  | False             | True        | 09-29-2016 | White,Black,Grey   |
		| Lofn    | False             | False       | 10-17-2015 | Grey,Purple        |
		| Skadi   | False             | False       | 11-19-2016 | White,Gold         |
		| Vor     | False             | False       | 12-21-2016 | Yellow,White       |
		| Ull     | False             | False       | 09-18-2015 | Black,Brown        |
		| Tyr     | True              | True        | 11-11-2016 | White,Yellow,Brown |
		| Sol     | False             | True        | 05-14-2016 | Green,Red,Blue     |
		| Nanna   | False             | True        | 09-08-2016 | Green,Orange       |
		| Kvasir  | True              | True        | 11-09-2015 | Yellow,Red         |
		| Ran     | False             | False       | 04-06-2016 | Pink,Blue          |
		| Hodr    | False             | False       | 01-01-2015 | White,Red          |
	And the following new customer
		| Name   | PreviouslyOrdered | WebCustomer | LastActive | FavouriteColours |
		| Ragnar | False             | True        | 01-01-2017 | Red,Black,Indigo  |

Scenario: When a new customer is added are they on the list?
	When I add add new customer
	And I make a request to get latest customers
	Then I should see the new customer in the results

Scenario: When a new customer is added is the user notified
	When I add add new customer
	And I make a request to get latest customers
	Then I should recieve a successful response