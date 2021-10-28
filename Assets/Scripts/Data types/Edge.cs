using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : IComparable<Edge> {
	public int priority; // smaller values are higher priority
	public int source;
	public int destination;
	public string lastName;

	public Edge(int priority, int source, int destination) {
		this.priority = priority;
		this.source = source;
		this.destination = destination;
	}

	public override string ToString() {
		return $"<NodeItem #{priority}, {source}:{destination}>";
	}

	public int CompareTo(Edge other) {
		if (priority < other.priority) return -1;
		else if (priority > other.priority) return 1;
		else return 0;
	}
}
