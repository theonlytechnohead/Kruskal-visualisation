using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudocodeSteps {

	enum state {
		function,
		disjointSetsADT,
		initSets,
		sortEdges,
		foreachEdge,
		ifDifferentSets,
		addEdge,
		unionSets,
		returnSets
	}

	public static int ProcessState(int cState) {
		var code = (CodeSteps.state)cState;
		switch (code) {
			case CodeSteps.state.function:
				return (int)state.function;
			case CodeSteps.state.initForest:
			case CodeSteps.state.initQueue:
				return (int)state.initSets;
			case CodeSteps.state.edgeForeach:
			case CodeSteps.state.enqueueEdge:
			case CodeSteps.state.endEdgeForeach:
				return (int)state.sortEdges;
			case CodeSteps.state.initUsedEdges:
			case CodeSteps.state.initCost:
			case CodeSteps.state.doWhile:
				return (int)state.foreachEdge;
			case CodeSteps.state.peekEdge:
			case CodeSteps.state.initTree1:
			case CodeSteps.state.initTree2:
			case CodeSteps.state.doForeach:
			case CodeSteps.state.checkTree1:
			case CodeSteps.state.applyTree1:
			case CodeSteps.state.checkTree2:
			case CodeSteps.state.applyTree2:
			case CodeSteps.state.checkSameTree:
			case CodeSteps.state.applySameTree:
			case CodeSteps.state.endForeach:
			case CodeSteps.state.checkTwoTrees:
				return (int)state.ifDifferentSets;
			case CodeSteps.state.mergeTrees:
				return (int)state.addEdge;
			case CodeSteps.state.removeMergedTree:
			case CodeSteps.state.incrementUsedEdges:
			case CodeSteps.state.popEdge:
			case CodeSteps.state.updateCost:
			case CodeSteps.state.endIf:
				return (int)state.unionSets;
			case CodeSteps.state.checkWhile:
				return (int)state.foreachEdge;
			case CodeSteps.state.returnCost:
			case CodeSteps.state.end:
				return (int)state.returnSets;
			default:
				return 0;
		}
	}
}
